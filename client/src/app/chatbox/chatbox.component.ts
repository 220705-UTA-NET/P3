import { Component, OnInit, ChangeDetectorRef} from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import {ChatService} from "../services/chat.service";
import { ChatMessage } from '../models/ChatDTO';

@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.css'],
  providers: [ChatService]
})

export class ChatboxComponent implements OnInit {
  testUsernames : string[] = [ 'Lance', 'Kadin', 'Joseph', 'Onandi', 'Rich', 'Ian', 'Jonathan'];
  user : string = ''; //Client username goes here
  //messages : ChatMessage[] = []; //Message history/log (needs subscribe)
  messages : ChatMessage[] = this.chatService.messages;
  sendContents : string = ''; //Don't touch this

  constructor(public chatService: ChatService, private cdref: ChangeDetectorRef) { }
  
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  ngOnInit(): void {
    this.user = this.testUsernames[Math.floor(Math.random() * this.testUsernames.length)];
    console.log(this.user);
    //start of code test
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
    }); //End of code test

    this.chatService.connect()
  }

  // grab values from chat.services
  tickets = this.chatService.userTickets;

  // will need to actively listen for changes to this?
  currentActiveTicket: number = this.chatService.currentActiveTicket;

  messageInput = new FormControl('');
  // public submitMessageKadin() {
  //   // user field of message will need to be changed when the user logs in
  //   const ticketId: number = this.chatService.currentActiveTicket;
  //   console.log(event)

  //   const message: ChatMessage = {
  //     user: "submitted user",
  //     message: this.messageInput.value as string
  //   }
    
  //   console.log(ticketId);
  //   const json = JSON.stringify(message)
  //   this.chatService.sendChat(json, ticketId)
  // }

  submitMessage(form: NgForm){
    const ticketId: number = this.chatService.currentActiveTicket;
    let newMessage: ChatMessage = {
      user: this.user,
      message: this.sendContents
    }
    console.log(this.user + ": " + this.sendContents);
    this.chatService.sendChat(newMessage, ticketId)
    form.reset();
  }

  // creates a new ticket for USER only
  public initiateTicket() {
    this.chatService.initiateTicket();
  }

  // should be called in init for TECH only; connects them to techSupport channel
  public joinTechSupport() {
    this.chatService.joinTechSupport();
  }

  // should be called by TECH only on click of a ticket to join a particular chat channel
  // will need to save the privateRoomKey variable saved in the web socket to the ticket, and transfer it on click
  public initializeSupportConnection(event: any) {
    const privateRoomKey: string = event.target.id

    this.chatService.initializeSupportConnection(parseInt(privateRoomKey));
  }
}
