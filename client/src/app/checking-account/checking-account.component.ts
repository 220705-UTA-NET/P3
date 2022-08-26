import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

export interface Checking {
  id: number,
  firstname: string,
  lastname: string,
  balance: number,
  deposits: number,
  withdrawals: number
}


@Component({
  selector: 'app-checking-account',
  templateUrl: './checking-account.component.html',
  styleUrls: ['./checking-account.component.css']
})
export class CheckingAccountComponent  {

  constructor(private http: HttpClient) { }

  checkingAcc: Checking = {
    id: 0,
    firstname: "",
    lastname: "",
    balance: 100,
    deposits: 10,
    withdrawals: 15
  };

  getCheckingAcc(value: any) {
    this.http.get(`http://localhost:4200/checkingaccount`, {
      params: new HttpParams().set('customerId', value),
      observe: "response",
      responseType: "json"
    }).subscribe((result: any)=> {
      console.log(result);
      this.checkingAcc = result.body;
    })
  
  }
  

}
