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

  constructor(private chatService: ChatService) { }

  ngOnInit(): void {
    this.chatService.connect()
    console.log("running connection")
  }
  messageInput = new FormControl('');
  submitMessage() {
    // this.chatService.send(this.messageInput.value as string)
    const message: ChatMessage = {
      user: "kadin",
      message: this.messageInput.value as string
    }

    const json = JSON.stringify(message)
    this.chatService.send(json)
  }

}
