import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import { Transaction } from '../models/transactions';
import { MyTransactionsService, TransactionResponse } from '../services/my-transactions.service';
//import {TransactionResponse} from '../services/my-transactions.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-my-transactions',
  templateUrl: './my-transactions.component.html',
  styleUrls: ['./my-transactions.component.css']
})
export class MyTransactionsComponent implements OnInit {
  pageSize = 5;
  page: number = 1;

  public myTransactions!: Transaction[];
  @Input() accountId! : number; //currently hardcoding customerid = 1
  @Output() updateAccount = new EventEmitter<number>();

  private url:string = "https://localhost:7249/API/Transactions/TransactionHistory?INPUT_AuthToken=1&INPUT_AccountNumber="; 
  //this is the URL to reach the end
  //temp connection string https://localhost:7249/API/Transactions/TransactionHistory?INPUT_AuthToken=1&INPUT_AccountNumber=1
  
  constructor(private myTransactionService:MyTransactionsService, private http: HttpClient) {
    this.myTransactions = [];
  }

  getTransactions(accountId : number){
    this.http.get<TransactionResponse>(this.url + accountId).subscribe(val => {
      this.myTransactions = val.lisT_DMODEL_Transactions;
    });
    // console.log(accountId);
    // this.myTransactions.push( { transaction_id:"1",
    // account_id:"9",
    // time:"10:15:21 Aug 1, 2020",
    // amount:"100.55",
    // transaction_notes:"deposit check#1",
    // transaction_type:"deposit",
    // completion_status:"complete"});
  }

  updateBalance(number: number){
    this.updateAccount.emit(number);
  }
  


  ngOnInit(): void {
    console.log(this.url + this.accountId);
    this.getTransactions(this.accountId);
  }
}
// export interface TransactionResponse{
//   NumberOfTransactions: number
//   LIST_DMODEL_Transactions: Array<Transaction>
// }