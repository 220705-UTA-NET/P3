import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'; 
import {ChatMessage} from "../models/ChatDTO";

// may not need to use either of the two below
// import { HttpClient } from '@microsoft/signalr';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ChatService {

  private _hubConnection!: signalR.HubConnection;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.connect()
  }

  send(message: string) {
    // params for send : hub method & message
    console.log("sending...")

    this._hubConnection.send("send", message)
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    
    console.log("request preparing to fire")
    this.http.post("https://localhost:7249/api/chat/send", message, httpOptions)
      .subscribe((result) => {
        console.log(result)
      })
    console.log("request fired")
  }
  
  // holds message conversations
  // to be accessed in all other components
  public messages: ChatMessage[] = []
  connect() {
    // connect to hub in backend
    this._hubConnection = new signalR.HubConnectionBuilder()
    // withUrl requires hub connection url
      .withUrl("https://localhost:7249/chatsocket", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build()

    // params for receiving: hub method & callback
    this._hubConnection.on("receive", (message: ChatMessage) => {
      console.log(message)
    });

    // params for sending notifications: hub method & callback
    // this._hubConnection.send("ReceiveOne", "testUser", "testMessage")

    // starts listening for hub coorespondance
    this._hubConnection.start()
      .then(() => console.log("connection started"))
      .catch((err) => console.log("error receiving connection", err))

    console.log("starting connection")
  }
}
