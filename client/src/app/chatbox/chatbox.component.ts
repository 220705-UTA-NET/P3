import { Component, OnInit } from '@angular/core';
import {ChatService} from "../services/chat.service";

@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.css'],
  providers: [ChatService]
})
export class ChatboxComponent implements OnInit {

  constructor(private chatService: ChatService) { }

  ngOnInit(): void {
    console.log(this.chatService.messages)
  }

}
