import { Component } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

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

  addAccount(customerId: number){
    console.log(customerId);
    let headers = new HttpHeaders({ 'Content-Type' : 'application/json', 'Accept' : 'application/json'});
    let args = new HttpParams().set('customerId', customerId).set('accountType', 0);
    this.http.post( `https://localhost:7249/accounts?` + args.toString(), {
      headers: headers
    }).subscribe((result : any) => {
      console.log(result);
      this.accounts.push(result);
    })
  }


}
