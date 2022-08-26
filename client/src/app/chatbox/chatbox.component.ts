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

  // grab values from chat.services
  tickets = this.chatService.userTickets;

  // will need to actively listen for changes to this?
  currentActiveTicket: number = this.chatService.currentActiveTicket;

  messageInput = new FormControl('');
  public submitMessage() {
    // user field of message will need to be changed when the user logs in
    const ticketId: number = this.chatService.currentActiveTicket;
    console.log(event)

    const message: ChatMessage = {
      user: "submitted user",
      message: this.messageInput.value as string
    }
    
    console.log(ticketId);
    const json = JSON.stringify(message)
    this.chatService.sendChat(json, ticketId)
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
