import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'; 
import {ChatMessage} from "../models/ChatDTO";

@Injectable({
  providedIn: 'root'
})

export class ChatService {

  private _hubConnection!: signalR.HubConnection;

  constructor() {}

  private privateRoomKey: number = 0;

    // a method that should only be available for tech support users, allows them to hear new tickets being created
    public joinTechSupport()
    {
      console.log("joining tech support");
      this._hubConnection.invoke("JoinSupportChat");
    }  

  // we may need different send functions; 
  // one for submitting a ticket/firing first message
  // and another for submitting messages after the group has been established
  public initializeSupportConnection() {
    console.log("initializing support convo")
    // need to generate a privateRoomKey. should do it via the customer_id in production, but will generate a random one for testing
    this.privateRoomKey = Math.floor(Math.random() * 10000);

    console.log(this.privateRoomKey);

    this._hubConnection.invoke("OpenTicket", this.privateRoomKey)
  }

  public sendChat(message: string) {
    // params for send : hub method & message
    console.log("sending...", this._hubConnection.connectionId)

    this._hubConnection.invoke("SendChat", "kadin", message, this.privateRoomKey);
  }

  // holds message conversations
  // to be accessed in all other components
  public messages: ChatMessage[] = []
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

    this._hubConnection.on("ReceiveOne", (message: ChatMessage) => {
      // can render response messages from here
      this.messages.push(message);
      console.log("all messages, on receive", this.messages)
    });

    // other endpoints a user will need
    // listening to group response (how the user will connect to a tech support)
    this._hubConnection.on("OpenTicket", (privateRoomKey: string) => {
      console.log("OpenTicket", privateRoomKey)
    })

    // starts listening for hub coorespondance
    this._hubConnection.start()
      .then(() => console.log("connection started"))
      .catch((err) => console.log("error receiving connection", err))
  }

  // for support accounts, will need its own unique listeners
  // automatically subscribe to techSupport group messages
  // ability to hear when a new ticket is created
  // ability to respond to a ticket being opened
}
