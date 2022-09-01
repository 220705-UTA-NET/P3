import { Component, OnInit, ChangeDetectorRef} from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import {ChatService} from "../services/chat.service";
import { ChatMessage, OpenTicket } from '../models/ChatDTO';

@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.css'],
  providers: [ChatService]
})

export class ChatboxComponent implements OnInit {
  testUsernames : string[] = [ "Jonathan De_La_Cruz", "Kadin Campbell", "Annie Arayon-Calosa", "Hau Nguyen", "German Diaz",
  "Brandon Figueredo", "Alejandro Hernandez", "James Beitz", "Abanob Sadek", "Ian Seki", "Iqbal Ahmed", "Brandon Sassano",
  "Daniel Beidelschies", "Derick Xie", "Eunice Decena", "Aurel Npounengnong", "Samuel Jackson", "Ellery De_Jesus", "Rogers Ssozi",
  "Lance Gong", "Arthur Gao", "Jared Green", "Jake Nguyen", "Joseph Boye", "Onandi Stewart", "Andrew Grozdanov", "Richard Hawkins" ];
  user : string = ''; //Client username goes here
  messages : ChatMessage[] = this.chatService.messages;
  sendContents : string = ''; //Don't touch this
  minimized : boolean = true;
  ticketMinimized : boolean = true;
  isSupport : boolean = false;


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
  tickets = this.chatService.openTickets;
  currentActiveTicket: number = this.chatService.currentActiveTicket;

  messageInput = new FormControl('');

  // for tracking how many messages have been sent in a short time period, to help prevent bombarding our server
  spamFilterTracker = {
    initialTime: Date.now(),
    messageCount: 0,
    isSpam: false
  }
  
  submitMessage(form: NgForm){
    this.checkIfSpam();

    const ticketId: number = this.chatService.currentActiveTicket;
    const now = new Date();
    let newMessage: ChatMessage = {
      ticketId: this.user,
      user: this.user,
      message: this.sendContents,
      date: now
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

  checkIfSpam() {
    // if it has been at least 5 seconds since initial message, reset counter
    if (Math.abs(this.spamFilterTracker.initialTime - Date.now()) > 5000) {
      this.spamFilterTracker.initialTime = Date.now();
      this.spamFilterTracker.messageCount = 0;
      this.spamFilterTracker.isSpam = false;
    }

    // if a user has sent 6+ messages within 5000 miliseconds
    if (this.spamFilterTracker.messageCount > 5) {
      this.spamFilterTracker.isSpam = true; 
      console.log("Please wait a moment before trying to send another message")
      return;
    }
    
    this.spamFilterTracker.messageCount++;
  }

  // creates a new ticket for USER only
  public initiateTicket(initialMessage: ChatMessage) {
    this.chatService.initiateTicket(initialMessage);
  }

  // should be called in init for TECH only; connects them to techSupport channel
  public joinTechSupport() {
    this.chatService.joinTechSupport();
    this.isSupport = true;
  }

  // should be called by TECH only on click of a ticket to join a particular chat channel
  // will need to save the privateRoomKey variable saved in the web socket to the ticket, and transfer it on click
  public initializeSupportConnection(event: any) {
    const privateRoomKey: string = event.target.id
    // should contain the initial message that will be pushed into TECH messages
    const now = new Date();
    const initialMessage: ChatMessage = {
      ticketId: event.target.dataset.ticketId,
      user: event.target.dataset.user,
      message: event.target.innerText,
      date: now
    };

    this.chatService.initializeSupportConnection(parseInt(privateRoomKey), initialMessage);
  }

  //closes ticket
  closeTicket(chatRoom: OpenTicket, user: string) {
    if(chatRoom.open)
      this.chatService.closeTicket(chatRoom.chatRoomId, user);
  }

  //Button click function to minimize chat
  public minimizerClick(){
    if(this.minimized)
      this.minimized = false;
    else
      this.minimized = true;
  }

  
  //Button click function to minimize support tickets
  public ticketMinimizerClick(){
    if(this.ticketMinimized)
      this.ticketMinimized = false;
    else
      this.ticketMinimized = true;
  }
}
