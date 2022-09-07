import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Transaction } from '../models/transactions';
import { MyTransactionsService } from '../services/my-transactions.service';
//import {TransactionResponse} from '../services/my-transactions.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

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

  private url:string = "https://localhost:7249/API/Transactions/TransactionHistory?INPUT_AuthToken=1&INPUT_AccountNumber="; 
  //this is the URL to reach the end
  //temp connection string https://localhost:7249/API/Transactions/TransactionHistory?INPUT_AuthToken=1&INPUT_AccountNumber=1
  
  constructor(private myTransactionService:MyTransactionsService) {
  }

  getTransactions(accountId : number){
    this.myTransactionService.getMyTransactions(this.url + accountId).subscribe(val => {
      this.myTransactions = val.lisT_DMODEL_Transactions;
    });
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