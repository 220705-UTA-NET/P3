import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'; 
import {ChatMessage} from "../models/ChatDTO";

@Injectable({
  providedIn: 'root'
})

export class ChatService {

  private _hubConnection!: signalR.HubConnection;

  constructor() {}

  ngOnInit(): void {
    this.connect()
  }

  send(message: string) {
    // params for send : hub method & message
    console.log("sending...", this._hubConnection.connectionId)

    this._hubConnection.invoke("SendMessage1", "kadin", message);
  }
  
  // holds message conversations
  // to be accessed in all other components
  public messages: ChatMessage[] = []
  connect() {
    // connect to hub in backend
    this._hubConnection = new signalR.HubConnectionBuilder()
    // withUrl requires hub connection url
      .withUrl("https://localhost:7249/chatsocket", {
        skipNegotiation: false,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build()

    this._hubConnection.on("ReceiveOne", (message: ChatMessage) => {
      // can render response messages from here
      console.log("on receiveOne", message)
    });

    // starts listening for hub coorespondance
    this._hubConnection.start()
      .then(() => console.log("connection started"))
      .catch((err) => console.log("error receiving connection", err))
  }
}