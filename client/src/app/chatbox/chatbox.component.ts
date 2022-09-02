import { Component, OnInit, ChangeDetectorRef} from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import { ChatService } from "../services/chat.service";
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
  messages : ChatMessage[] = [];
  tickets: OpenTicket[] = [];
  // changed on ticket selection, routes message to correct user for TECH ONLY
  currentActiveTicket: string = this.chatService.currentActiveTicket;
  sendContents : string = ''; //Don't touch this
  minimized : boolean = true;
  ticketMinimized : boolean = true;
  isSupport : boolean = false;
  dateActive : boolean = false;

  constructor(public chatService: ChatService, private cdref: ChangeDetectorRef) { }
  
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  ngOnInit(): void {
    // TESTING ONLY; setting username (will be grabbed here from auth once implemented)
    this.user = this.testUsernames[Math.floor(Math.random() * this.testUsernames.length)];
    console.log(this.user);

    // subscribe to changes in tickets
    this.chatService.ticketService.subscribe((result) => this.tickets = result);
    //subscribe to changes in messages
    this.chatService.messageService.subscribe((result) => this.messages = result);

    // connect to webSocket
    this.chatService.connect()

    // fetch all tickets for TECH ONLY
    this.fetchAllTickets();
  }

  messageInput = new FormControl('');

  // for tracking how many messages have been sent in a short time period, to help prevent bombarding our server
  spamFilterTracker = {
    initialTime: Date.now(),
    messageCount: 0,
    isSpam: false
  }
  
  submitMessage(form: NgForm){
    const ticketId: string = this.chatService.currentActiveTicket;
    const now = new Date();

    if(this.sendContents == null)
      return;
    console.log(this.user + ": " + this.sendContents);

    const isSpam: boolean = this.checkIfSpam();

    if (!isSpam) {
      let newMessage: ChatMessage = {
        ticketId: ticketId,
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
      // this.messages.push(newMessage);
      this.chatService.sendChat(newMessage)
      form.reset();
    }
  }

  checkIfSpam(): boolean {
    // if it has been at least 5 seconds since initial message, reset counter
    let isSpam: boolean = false;

    if (Math.abs(this.spamFilterTracker.initialTime - Date.now()) > 5000) {
      this.spamFilterTracker.initialTime = Date.now();
      this.spamFilterTracker.messageCount = 0;
      this.spamFilterTracker.isSpam = false;
    }

    // if a user has sent 6+ messages within 5000 miliseconds
    const now = new Date();
    if (this.spamFilterTracker.messageCount > 5) {
      const announcement = {
        ticketId: this.user,
        user: "Announcement",
        message: "Please wait a moment before sending another message",
        date: now
      }

      this.chatService.sendChat(announcement);
      console.log("Please wait a moment before trying to send another message")
      
      isSpam = true;
    }
    
    this.spamFilterTracker.messageCount++;
    return isSpam;
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

    // connect tech support to user chat room
    this.chatService.initializeSupportConnection(privateRoomKey, initialMessage);
    // grab the user's ticket's previous message
    this.fetchUserTicket(event.target.dataset.user);
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
  
  public delay = async (ms : number) => new Promise(res => setTimeout(res, ms));
  // public onDateClick = async() => {
  //   if(!this.dateActive)
  //   {
  //     this.dateActive = true;
  //     await this.delay(3000);
  //     this.dateActive = false;
  //   }
  // }

  public onDateClick(){
    this.dateActive = true;
  }

  public onDateLeave(){
    this.dateActive = false;
  }

  public onHover = async() => {
    if(!this.dateActive)
    {
      this.dateActive = true;
      await this.delay(10);
      this.dateActive = false;
    }
  }

  public zeroPad = (num: number, places: number) => String(num).padStart(places, '0')
  public formatDate(date : Date): string{
    const then = new Date(date);
    const now = new Date();
    if(then.getFullYear() < now.getFullYear()){
      return then.getMonth() + "/" + this.zeroPad(then.getDay(), 2) + "/" + then.getFullYear();
    }
    if(then.getMonth() < (then.getMonth() - 1)){
      return then.getMonth() + "/" + this.zeroPad(then.getDay(), 2) + "/" + then.getFullYear();
    }
    if(then.getMonth() == (then.getMonth() - 1)){
      return "Last Month";
    }
    if(then.getDate() == now.getDate() - 1){
      return "Yesterday";
    }
    if(then.getDate() == now.getDate()){
      return "Today " + then.getHours() + ":" +  this.zeroPad(then.getMinutes(), 2);
    }
    if(now.getDate() - then.getDate() <= 7){
      const days : string[] = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
      return "This " + days[then.getDay()];
    }
    return "You don't exist in the space time continuum";
  }

  // will need to update how we get all tickets, as below & the previous method above (getting them from the service variable) collide
  public fetchAllTickets() {
    this.chatService.fetchAllTickets()
  }

  public fetchUserTicket(username: string) {
    this.chatService.fetchUserTicket(username)
  }
}
