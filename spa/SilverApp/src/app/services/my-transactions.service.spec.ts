import { TestBed } from '@angular/core/testing';

import { MyTransactionsService } from './my-transactions.service';

describe('MyTransactionsService', () => {
  let service: MyTransactionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyTransactionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
