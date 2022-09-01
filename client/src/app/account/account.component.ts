import { Component } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

export interface Account {
  accountId: number,
  type: number,
  balance: number,
  customerId: number
}


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {

  constructor(private http: HttpClient) { }

  accounts: Account[] = [];

  getAccounts(value: any) {
    this.http.get(`https://localhost:7249/accounts`, {
      params: new HttpParams().set('customerId', value),
      observe: "response",
      responseType: "json"
    }).subscribe((result: any) => {
      console.log(result);
      this.accounts = result.body;
      console.log(this.accounts);
    })

  }


}
