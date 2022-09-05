import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WARNING } from './budget-warning';

@Injectable({
  providedIn: 'root',
})
export class BudgetNotifyService {
  accounts: WARNING[] = [];

  constructor(private http: HttpClient) {}

  ngOnIt() {
    // if there's a warning, call showNotification
    // make a get request to server
  }

  makeRequest() {
    return this.http.get<WARNING[]>(
      'https://localhost:7249/Budget/GetCustomerBudgetsWarning/8' // need to change this to pass the users customer ID as a parameter
    );
  }
}
