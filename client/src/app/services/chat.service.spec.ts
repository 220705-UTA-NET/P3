import { TestBed } from '@angular/core/testing';

import { ChatService } from './chat.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('ChatService', () => {
  let service: ChatService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    })
    service = TestBed.inject(ChatService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should connect to hub', () => {
    service.connectedToHub = false;
    service.connect();
    expect(service.connectedToHub).toBe(true);
  })
});
