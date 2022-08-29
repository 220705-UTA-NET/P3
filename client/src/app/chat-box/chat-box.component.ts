import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
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
  constructor(private cdref: ChangeDetectorRef) { }
  
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

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

  submitMessage(form: NgForm){
    let newMessage: ChatMessage = {
      user: this.user,
      message: this.sendContents
    }
    console.log(this.user + ": " + this.sendContents);
    this.messages.push(newMessage);
    form.reset();
  }
}
