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
  messages : ChatMessage[] = this.chatService.messages;
  sendContents : string = ''; //Don't touch this
  minimized : boolean = true;

  constructor(public chatService: ChatService, private cdref: ChangeDetectorRef) { }
  
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  ngOnInit(): void {
    this.user = this.testUsernames[Math.floor(Math.random() * this.testUsernames.length)];
    console.log(this.user);
    this.chatService.connect()
  }

  // grab values from chat.services
  // tickets = this.chatService.userTickets;
  tickets = this.chatService.openTickets;

  // will need to actively listen for changes to this?
  currentActiveTicket: number = this.chatService.currentActiveTicket;

  messageInput = new FormControl('');

  submitMessage(form: NgForm){
    const ticketId: number = this.chatService.currentActiveTicket;
    let newMessage: ChatMessage = {
      user: this.user,
      message: this.sendContents
    }
    if(this.sendContents == null)
      return;
    console.log(this.user + ": " + this.sendContents);

    // for first message, automatically submit a ticket
    if (this.messages.length === 0) {
      this.initiateTicket(newMessage)
    }
    this.chatService.sendChat(newMessage, ticketId)
    form.reset();
  }

  // creates a new ticket for USER only
  public initiateTicket(initialMessage: ChatMessage) {
    this.chatService.initiateTicket(initialMessage);
  }

  // should be called in init for TECH only; connects them to techSupport channel
  public joinTechSupport() {
    this.chatService.joinTechSupport();
  }

  // should be called by TECH only on click of a ticket to join a particular chat channel
  // will need to save the privateRoomKey variable saved in the web socket to the ticket, and transfer it on click
  public initializeSupportConnection(event: any) {
    const privateRoomKey: string = event.target.id

    // should contain the initial message that will be pushed into TECH messages
    const initialMessage: ChatMessage = {
      user: event.target.dataset.user,
      message: event.target.innerText
    };

    this.chatService.initializeSupportConnection(parseInt(privateRoomKey), initialMessage);
  }

  closeTicket(chatRoomId: string) {
    this.chatService.closeTicket(chatRoomId);
  }

  public minimizerClick(){
    if(this.minimized)
      this.minimized = false;
    else
      this.minimized = true;
  }
}
