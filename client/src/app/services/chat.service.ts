import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'; 
import {ChatMessage} from "../models/ChatDTO";

// may not need to use either of the two below
import { HttpClient } from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ChatService {

  private _hubConnection!: signalR.HubConnection;

  constructor() {}

  ngOnInit(): void {
    this.connect()
  }

  onSend() {
    // params for send : hub method & message
    this._hubConnection.send("send message", "test message")
  }
  
  // holds message conversations
  // to be accessed in all other components
  public messages: ChatMessage[] = []
  connect() {
    // connect to hub in backend
    this._hubConnection = new signalR.HubConnectionBuilder()
    // withUrl requires hub connection url
      .withUrl("")
      .build()

    // params for receiving: hub method & callback
    this._hubConnection.on("receive", (message: ChatMessage) => {
      console.log(message)
    });

    this._hubConnection.start()
      .then(() => console.log("connection started"))
      .catch((err) => console.log("error receiving connection", err))
  }

  // need to make messages observable to be usable throughout the project
  // since this is a service, can't the public variable of messages simply be injected?
}
