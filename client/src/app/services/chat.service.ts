import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'; 
import {ChatMessage} from "../models/ChatDTO";

@Injectable({
  providedIn: 'root'
})

export class ChatService {

  private _hubConnection!: signalR.HubConnection;

  constructor() {}

  // for USERS only, establishes their private room to be connected to
  private privateRoomKey: number = 0;

  // for TECH only. testing user's privateRoomKey, will need to be an array later on to hold all users.
  public testRoomKey: number = 0;

  // a method that should only be available for tech support users, allows them to hear new tickets being created
  public joinTechSupport()
  {
    console.log("joining tech support");
    this._hubConnection.invoke("JoinSupportChat");
  }  

  // for USERS only; puts user in a private connection room & informs tech support
  public initiateTicket() {
    console.log("initializing support convo")
    // need to generate a privateRoomKey. should do it via the customer_id in production, but will generate a random one for testing
    this.privateRoomKey = Math.floor(Math.random() * 10000);

    // for testing a tech support making a chat room only
    this.testRoomKey = this.privateRoomKey

    this._hubConnection.invoke("OpenTicket", this.privateRoomKey)
  }

  // for TECH only; on click of a ticket, matches a tech support with a customer
  public initializeSupportConnection(privateRoomKey: number) {
    console.log("connecting tech support with user", privateRoomKey);
    this._hubConnection.invoke("TechSupportJoinsConversation", privateRoomKey)
  }

  // holds message conversations
  public messages: ChatMessage[] = []
  // will be utilized by both USERS & TECH, once in a connected room
  public sendChat(message: string) {
  // params for send : hub method & message
  console.log("sending...", this._hubConnection.connectionId)

  this._hubConnection.invoke("SendChat", "kadin", message, this.privateRoomKey);
  }

  public connect() {
    // connect to hub in backend
    this._hubConnection = new signalR.HubConnectionBuilder()
    // withUrl requires hub connection url
      .withUrl("https://localhost:7249/chatsocket", {
        // cannot access the connectionId in the backend if skipNegation: true
        skipNegotiation: false,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build()

    // listening for messages in private chat room
    this._hubConnection.on("messaging", (message: ChatMessage) => {
      // can render response messages from here
      this.messages.push(message);
      console.log("all messages, on receive", this.messages)
    });

    // listening for when a user opens a ticket, need the privateRoomKey id that will be attached
    this._hubConnection.on("OpenTicket", (privateRoomKey: number) => {
      console.log("OpenTicket", privateRoomKey)
      this.testRoomKey = privateRoomKey;
    })

    // starts listening for hub coorespondance
    this._hubConnection.start()
      .then(() => console.log("connection started"))
      .catch((err) => console.log("error receiving connection", err))
  }
}
