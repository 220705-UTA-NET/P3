import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule , HttpTestingController} from '@angular/common/http/testing';

import { ChatboxComponent } from './chatbox.component';
import { ChatService } from '../services/chat.service';
import {OpenTicket, ChatMessage} from "../models/ChatDTO";

describe('ChatboxComponent', () => {
  let component: ChatboxComponent;
  let fixture: ComponentFixture<ChatboxComponent>;
  let httpTestingController: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChatboxComponent ],
      imports: [HttpClientTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatboxComponent);
    component = fixture.componentInstance;

    httpTestingController = TestBed.inject(HttpTestingController);

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should check if spam, return false', () => {
    component.spamFilterTracker.messageCount = 0;
    expect(component.checkIfSpam()).toBe(false);

    component.spamFilterTracker.messageCount = 6;
    expect(component.checkIfSpam()).toBe(true);
  });

  it('should minimize chat', () => {
    component.minimized = false;
    component.minimizerClick();
    expect(component.minimized).toBe(true);

    component.minimizerClick();
    expect(component.minimized).toBe(false);
  });

  it('should minimize ticket', () => {
    component.ticketMinimized = false;
    component.ticketMinimizerClick();
    expect(component.ticketMinimized).toBe(true);

    component.ticketMinimizerClick();
    expect(component.ticketMinimized).toBe(false);
  });

  it('should control date activation', () => {
    component.dateActive = false;
    component.onDateClick();
    expect(component.dateActive).toBe(true);

    component.onDateLeave();
    expect(component.dateActive).toBe(false);
  });

  it('should temporarily display date', () => {
    component.dateActive = false;
    component.onHover();
    expect(component.dateActive).toBe(true);
    // component.delay(11);
    // expect(component.dateActive).toBe(false);
  });

  it('should fetch all tickets', () => {
    // let spyCall = spyOn(component, "fetchAllTickets").and.callThrough();

    // fixture.whenStable().then(() => {
    //   expect(spyCall).toHaveBeenCalled();
    // })

    // let mockAllTickets: OpenTicket[] = [];
    // let mockTicket: OpenTicket = {
    //   chatRoomId: "Kadin",
    //   user: "Kadin",
    //   message: "Mocking fetch all tickets",
    //   open: true
    // }
    // mockAllTickets.push(mockTicket);

    // const request = httpTestingController.expectOne(data => 
    //   data.url === "https://localhost:7249/tickets" && data.method === "GET"
    // );

    // request.flush(mockAllTickets);
  })
});
