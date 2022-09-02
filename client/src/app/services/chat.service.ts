import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'; 
import { BehaviorSubject } from 'rxjs';
import {ChatMessage, OpenTicket} from "../models/ChatDTO";

@Injectable({
  providedIn: 'root'
})

export class ChatService {

  constructor(private http: HttpClient) {}

  // BOTH USER and TECH --------------------------------------------------------
  private _hubConnection!: signalR.HubConnection;

  public messageService = new BehaviorSubject<ChatMessage[]>([]);
  // holds message conversations
  public messages: ChatMessage[] = []

  public ticketService = new BehaviorSubject<OpenTicket[]>([]);
  // holds the chatRoomId & initial message for TECH ONLY
  public openTickets: OpenTicket[] = [];

  // how messages are exchanged between tech & user once in a private room
  public sendChat(chat: ChatMessage) {
    if (this.currentActiveTicket != "") {
      this._hubConnection.invoke("SendChat", chat, this.currentActiveTicket);
    } else {
      this._hubConnection.invoke("SendChat", chat, this.privateRoomKey);
    }
  }

  // USER ONLY -----------------------------------------------------------------
  // establishes their private room to be connected to
  private privateRoomKey: string = "";

  // for USERS only; puts user in a private connection room & informs tech support
  public initiateTicket(initialMessage: ChatMessage) {    
    // need to get user login & set to privateRoomKey
    this.privateRoomKey = initialMessage.user;

    this._hubConnection.invoke("OpenTicket", this.privateRoomKey, initialMessage)
  }

  // TECH ONLY -----------------------------------------------------------------
  // holds all user tickets
  public userTickets: string[] = [];
  // value of active ticket is assigned to the send message btn; how messages are routed correctly
  public currentActiveTicket: string = "";

  // enables tech support to be notified when a new ticket is made.
  public joinTechSupport()
  {
    this._hubConnection.invoke("JoinSupportChat");
  }  

  // on click of a ticket, matches a tech support with a customer
  public initializeSupportConnection(roomKey: string, initialMessage: ChatMessage) {
    // sets currentActiveTicket to the user's roomKey; how we change who the TECH is speaking to
    this.currentActiveTicket = roomKey;
    this._hubConnection.invoke("TechSupportJoinsConversation", roomKey);

    // clear previous message history with other clients
    this.messages.length = 0;

    // move initial message into TECH's chat
    this.messages.push(initialMessage)
    this.messageService.next(this.messages);
  }

  // once a ticket has been fulfilled, close the ticket connection
  public closeTicket(chatRoomId: string, user: string) {
    this.openTickets.forEach((ticket) => {
      if (ticket.chatRoomId === chatRoomId) {
        ticket.open = false;
      }
    })

    const now = new Date();
    const finalMessage: ChatMessage = {
      ticketId: chatRoomId,
      user: "Announcement",
      message: "This ticket has been marked as resolved",
      date: now
    }

    // CloseTicket method simply informs group members that the ticket has been closed
    this._hubConnection.invoke("CloseTicket", chatRoomId, finalMessage);
  }

  // connection ----------------------------------------------------------------
  // establishes connection to websocket / hub, and establishes event listeners
  // the events that should be listened to will depend on USER vs. TECH
  // can check for user status & create different connection methods for each
  public connect() {
    this._hubConnection = new signalR.HubConnectionBuilder()
    // withUrl requires hub connection url
      .withUrl("https://localhost:7249/chatsocket", {
        // cannot access the connectionId in the backend if skipNegation: true
        skipNegotiation: false,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build()

    // BOTH: listening for messages in private chat room
    this._hubConnection.on("messaging", (message: ChatMessage) => {
      // can render response messages from here
      this.messages.push(message);
      this.messageService.next(this.messages);
    });

    // BOTH: tech joins private room & notifies both parties
    this._hubConnection.on("conversationStarted", (message: ChatMessage) => {
      // should announce to user that tech support has joined the chat session
      
      const now = new Date();
      const joinAnnouncement: ChatMessage =  {
        ticketId: message.ticketId,
        user: "Announcement",
        message: "Tech support has joined the chat",
        date: now
      };

      this.messages.push(joinAnnouncement);
      this.messageService.next(this.messages);
    })

    // TECH: listening for when a user opens a ticket, need the privateRoomKey id that will be attached
    // Ticket also carries the user's name & their initial message that began the chat
    this._hubConnection.on("OpenTicket", (privateRoomKey: string, initialMessage: ChatMessage) => {
      this.userTickets.push(privateRoomKey);
      this.privateRoomKey = privateRoomKey;

      const newTicket: OpenTicket = {
        chatRoomId: privateRoomKey,
        user: initialMessage.user,
        message: initialMessage.message,
        open: true
      }
      this.openTickets.push(newTicket);
      this.ticketService.next(this.openTickets);
    })

    // notify all participants that the ticket has been resolved
    this._hubConnection.on("CloseTicket", (notification: ChatMessage) => {
      this.messages.push(notification);
      this.messageService.next(this.messages);
    })

    // BOTH: starts listening for hub coorespondance
    this._hubConnection.start()
      .then(() => console.log("connection started"))
      .catch((err) => console.log("error receiving connection", err))
  }

  // api calls

  // get all currently open tickets
  public fetchAllTickets() {
    return this.http.get("https://localhost:7249/tickets", {
      // headers: {"Authorization": accessToken},
      observe: "response"
    })
      .subscribe((result) => {
        console.log("fetch all tickets result", result);
        const receivedTickets: OpenTicket[] = result.body as OpenTicket[];
        // allTickets to be displayed to TECH user; rendered in the HTML
        this.openTickets = receivedTickets;
      })
  }

  // get particular ticket by username
  public fetchUserTicket(username: string) {
    return this.http.get(`https://localhost:7249/message?key=${username}`, {
      // headers: {"Authorization": accessToken},
      observe: "response"
    })
      .subscribe((result) => {
        const userTicket: OpenTicket = result.body as OpenTicket;

        // new ChatMessage[] to replace the currently set one
        const ticketMessages: ChatMessage[] = [];
        // create a new initialTicket to push to ticketMessages; need to typecast OpenTicket -> ChatMessages
        const initialTicketMessage: ChatMessage = {
          ticketId: userTicket.chatRoomId,
          user: userTicket.user,
          message: userTicket.message,
          date: new Date()
        }
        ticketMessages.push(initialTicketMessage);

        // overwrite currently set messages with the initial message from the newly selected ticket
        this.messages = ticketMessages;
        this.messageService.next(this.messages);
      })
  }

  // functionality for saving messages & creating tickets will be handled in the HUB, as it is taken care of by websocket connection
}
