import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'; 
import {ChatMessage} from "../models/ChatDTO";

@Injectable({
  providedIn: 'root'
})

export class ChatService {

  constructor() {}

  // BOTH USER and TECH --------------------------------------------------------
  private _hubConnection!: signalR.HubConnection;

  // holds message conversations
  public messages: ChatMessage[] = []

  // how messages are exchanged between tech & user once in a private room
  public sendChat(message: string) {
    // params for send : hub method & message
    console.log("sending...", this._hubConnection.connectionId)

    // temporary measure until we have seperate profiles
    if (this.privateRoomKey == 0) {
      // TECH KEY
      console.log("testRoomKEY FOR TECH", this.testRoomKey)
      this._hubConnection.invoke("SendChat", "TECH", message, this.testRoomKey);
    } else {
      // USER KEY
      console.log("PRIVATEROOM KEY FOR USER", this.privateRoomKey)
      this._hubConnection.invoke("SendChat", "USER", message, this.privateRoomKey);
    }
  }

  // USER ONLY -----------------------------------------------------------------
  // establishes their private room to be connected to
  private privateRoomKey: number = 0;

  // for USERS only; puts user in a private connection room & informs tech support
  public initiateTicket() {
    console.log("initializing ticket")
    
    // need to generate a privateRoomKey. should do it via the customer_id in production, but will generate a random one for testing
    this.privateRoomKey = Math.floor(Math.random() * 10000);

    // for testing a tech support making a chat room only
    this.testRoomKey = this.privateRoomKey

    this._hubConnection.invoke("OpenTicket", this.privateRoomKey)
  }

  // TECH ONLY -----------------------------------------------------------------
  // testing user's privateRoomKey, will need to be an array later on to hold all users currently with tickets.
  public testRoomKey: number = 0;

  // enables tech support to be notified when a new ticket is made.
  public joinTechSupport()
  {
    console.log("joining tech support");
    this._hubConnection.invoke("JoinSupportChat");
  }  

  // on click of a ticket, matches a tech support with a customer
  public initializeSupportConnection(roomKey: number) {
    console.log("connecting tech support with user", roomKey);
    this._hubConnection.invoke("TechSupportJoinsConversation", roomKey)
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
      console.log("all messages, on receive", this.messages)
    });

    // BOTH: tech joins private room & notifies both parties
    this._hubConnection.on("conversationStarted", (message: ChatMessage) => {
      console.log("conversation started:", message);
    })

    // TECH: listening for when a user opens a ticket, need the privateRoomKey id that will be attached
    this._hubConnection.on("OpenTicket", (privateRoomKey: number) => {
      console.log("OpenTicket", privateRoomKey)
      this.testRoomKey = privateRoomKey;
    })

    // BOTH: starts listening for hub coorespondance
    this._hubConnection.start()
      .then(() => console.log("connection started"))
      .catch((err) => console.log("error receiving connection", err))
  }
}
