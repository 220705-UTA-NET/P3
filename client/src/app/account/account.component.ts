import { Component, Input } from '@angular/core';
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
  @Input() customerId! : number;
  public amount : number = 0;

  constructor(private http: HttpClient) { 
    let login = localStorage.getItem('customer') as string;
    let customer = JSON.parse(login);

    if(customer === null){
      this.customerId = 1;
      this.getAccounts();
    }else{
      this.getAccounts();
    }
  }

  accounts: Account[] = [];

  getAccounts() {
    this.http.get(`https://localhost:7249/accounts`, {
      params: new HttpParams().set('customerId', this.customerId),
      observe: "response",
      responseType: "json"
    }).subscribe((result: any) => {
      console.log(result);
      this.accounts = result.body;
      console.log(this.accounts);
    })

  }

  addAccount(){
    let headers = new HttpHeaders({ 'Content-Type' : 'application/json', 'Accept' : 'application/json'});
    let args = new HttpParams().set('customerId', this.customerId).set('accountType', 0);
    this.http.post( `https://localhost:7249/accounts?` + args.toString(), {
      headers: headers
    }).subscribe((result : any) => {
      console.log(result);
      this.accounts.push(result);
    })
  }

  toggleInputField(name: string){
    document.getElementById(name)?.classList.toggle("show");
  }
  
  withdraw(){
    console.log(0 - this.amount);
  }

  deposit(){
    console.log(this.amount);
  }


}
