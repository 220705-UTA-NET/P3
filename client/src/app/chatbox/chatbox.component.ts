import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import {ChatService} from "../services/chat.service";
import { ChatMessage } from '../models/ChatDTO';

@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.css'],
  providers: [ChatService]
})

export class ChatboxComponent implements OnInit {

  constructor(public chatService: ChatService) { }

  ngOnInit(): void {
    this.chatService.connect()
  }

  messageInput = new FormControl('');
  public submitMessage() {
    // user will need to be changed when the user logs in
    const message: ChatMessage = {
      user: "submitted user",
      message: this.messageInput.value as string
    }

    const json = JSON.stringify(message)
    this.chatService.sendChat(json)
  }

  public initiateTicket() {
    this.chatService.initiateTicket();
  }

  public joinTechSupport() {
    this.chatService.joinTechSupport();
  }

  // for tech support users ONLY
  // will need to save the privateRoomKey variable saved in the web socket to the ticket, and transfer it on click
  public initializeSupportConnection(event: any) {
    const privateRoomKey: number = event.target.id
    this.chatService.initializeSupportConnection(privateRoomKey);
  }

}
