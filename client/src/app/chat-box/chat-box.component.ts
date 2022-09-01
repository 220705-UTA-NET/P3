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
    const now = new Date();
    let testMessages: ChatMessage[] = [
      {
        ticketId: this.user,
        user: 'Lance',
        message: 'hello',
        date: now
      },
      {
        ticketId: this.user,
        user: 'Kadin',
        message: 'hello to you too',
        date: now
      },
      {
        ticketId: this.user,
        user: 'Lance',
        message: 'cool',
        date: now
      },
      {
        ticketId: this.user,
        user: 'Lance',
        message: 'overflow',
        date: now
      },
      {
        ticketId: this.user,
        user: 'Onandi',
        message: 'overflow?',
        date: now
      },      
      {
        ticketId: this.user,
        user: 'Lance',
        message: 'overflow.',
        date: now
      },
      {
        ticketId: this.user,
        user: 'Lance',
        message: 'overflow',
        date: now
      },
      {
        ticketId: this.user,
        user: 'Onandi',
        message: 'overflow?',
        date: now
      },      
      {
        ticketId: this.user,
        user: 'Lance',
        message: 'overflow.',
        date: now
      }
    ];

    testMessages.forEach((msg: ChatMessage) => {
      this.messages.push(msg);
    });
  }

  submitMessage(form: NgForm){
    const now = new Date();
    let newMessage: ChatMessage = {
      ticketId: this.user,
      user: this.user,
      message: this.sendContents,
      date: now
    }
    console.log(this.user + ": " + this.sendContents);
    this.messages.push(newMessage);
    form.reset();
  }
}
