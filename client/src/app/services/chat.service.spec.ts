// import { TestBed } from '@angular/core/testing';

// import { ChatService } from './chat.service';
// //import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
// //import { ChatMessage } from '../models/ChatDTO';

// describe('ChatService', () => {
//   let service: ChatService;
//   // let httpMock: HttpTestingController;

//   beforeEach(() => {
//     TestBed.configureTestingModule({
//       imports: [HttpClientTestingModule]
//     })
//     service = TestBed.inject(ChatService);
//   });

//   it('should be created', () => {
//     expect(service).toBeTruthy();
//   });

//   it('should connect to hub', () => {
//     service.connectedToHub = false;
//     service.connect();
//     expect(service.connectedToHub).toBe(true);
//   });

//   it('should call fetch all tickets', () => {
//     const fetchAllTicketsSpy = spyOn(service, "fetchAllTickets");
//     service.fetchAllTickets();
//     expect(fetchAllTicketsSpy).toHaveBeenCalled();
//   });

//   it('should call fetchUserTicket', () => {
//     const fetchUserTicketSpy = spyOn(service, "fetchUserTicket");
//     service.fetchUserTicket("kadin");
//     expect(fetchUserTicketSpy).toHaveBeenCalled();
//   });

//   it('should call saveChatMessage', () => {
//     const fakeMessage:ChatMessage = {
//       ticketId: "kadin",
//       user: "kadin",
//       message: "Hello, World",
//       date: new Date()
//     }

//     const saveChatMessageSpy = spyOn(service, "saveChatMessage");
//     service.saveChatMessage(fakeMessage);
//     expect(saveChatMessageSpy).toHaveBeenCalled();
//   })
// });
