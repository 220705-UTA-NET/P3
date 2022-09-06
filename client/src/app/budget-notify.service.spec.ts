import { TestBed } from '@angular/core/testing';

import { BudgetNotifyService } from './budget-notify.service';

describe('BudgetNotifyService', () => {
  let service: BudgetNotifyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BudgetNotifyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
