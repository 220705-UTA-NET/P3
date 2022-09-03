import { HttpClient, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { BudgetService } from './budget.service';
import { Budget } from './models/budget';

describe('BudgetService', () => {
  let expectedBudget = {budgetId:1,customerId:1,accountId:1,monthlyAmount:100,warningAmount:50,remaining:80,startDate:new Date()};
  let service: BudgetService;
  let httpClientSpy : jasmine.SpyObj<HttpClient>;
  const responseOK = JSON.parse('{"status":"200"}');
  const status201 = JSON.parse('{"status":"201"}');
  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient',['get','put','post','delete']);
    service = new BudgetService(httpClientSpy);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  it('should return budget',()=>{
    httpClientSpy.get.and.returnValue(new BehaviorSubject<[Budget]>([expectedBudget]));
    service.getBudgetList(1).subscribe({
      next: res =>{
        expect(res).toEqual([expectedBudget]);
      }
    })
  });
  it('should update budget',()=>{
    httpClientSpy.put.and.returnValue(new BehaviorSubject(responseOK));
    service.updateBudget(expectedBudget).subscribe({
      next:res =>{
        expect(res.status).toBe(responseOK.status);
      }
    })
  });
  it('should update length to next id',()=>{
    service.updateLength(1);
    expect(service.getNextId()).toEqual(2);
  });
  it('should post new budget',()=>{
    httpClientSpy.post.and.returnValue(new BehaviorSubject(status201));
    service.addNewBudget(expectedBudget).subscribe({
      next: res=>{
        expect(res.status).toEqual(status201.status);
      }
    });
  });
  it('should delete',()=>{
    httpClientSpy.delete.and.returnValue(new BehaviorSubject(responseOK));
    service.deleteBudget(expectedBudget).subscribe({
      next: res=>{
        expect(res.status).toEqual(responseOK.status);
      }
    })
  })
});
