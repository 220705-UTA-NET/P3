import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ChatMessage } from '../models/ChatDTO';

@Component({
  selector: 'app-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.css']
})


export class ChatBoxComponent implements OnInit {

  user : string = 'Lance';
  messages : ChatMessage[] = [];
  sendContents : string = '';
  constructor() { }

  ngOnInit(): void {

    let testMessages: ChatMessage[] = [
      {
        user: 'Lance',
        message: 'hello'
      },
      {
        user: 'Kadin',
        message: 'hello to you too'
      },
      {
        user: 'Lance',
        message: 'cool'
      },
      {
        user: 'Lance',
        message: 'overflow'
      },
      {
        user: 'Onandi',
        message: 'overflow?'
      },      
      {
        user: 'Lance',
        message: 'overflow.'
      },
      {
        user: 'Lance',
        message: 'overflow'
      },
      {
        user: 'Onandi',
        message: 'overflow?'
      },      
      {
        user: 'Lance',
        message: 'overflow.'
      }
    ];

    testMessages.forEach((msg: ChatMessage) => {
      this.messages.push(msg);
    });
  }

  submitMessage(){
    let newMessage: ChatMessage = {
      user: this.user,
      message: this.sendContents
    }
    console.log(this.user + ": " + this.sendContents);
    this.messages.push(newMessage); 
  }
}
