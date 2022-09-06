import { Component, Input, OnInit } from '@angular/core';
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
  pageSize = 2;
  page: number = 1;

  myTransactions: Transaction[] = [];
  @Input() accountId! : number; //currently hardcoding customerid = 1
  private url:string = "https://localhost:7249/API/Transactions/TransactionHistory?INPUT_AuthToken=1&INPUT_AccountNumber="; 
  //this is the URL to reach the end
  //temp connection string https://localhost:7249/API/Transactions/TransactionHistory?INPUT_AuthToken=1&INPUT_AccountNumber=1
  
  constructor(private myTransactionService:MyTransactionsService) { 
    //populate the real customerid here after authentication is done.
  }
  trans = [
    { transaction_id:"1",
      account_id:"1",
      time:"10:15:21 Aug 1, 2020",
      amount:"100.55",
      transaction_notes:"deposit check#1",
      transaction_type:"deposit",
      completion_status:"complete"},
    { transaction_id:"2",
      account_id:"1",
      time:"10:15:21 Sep 1, 2020",
      amount:"200.12",
      transaction_notes:"withdraw to buy foods",
      transaction_type:"deposit",
      completion_status:"complete"},
    { transaction_id:"3",
      account_id:"1",
      time:"10:15:21 Aug 1, 2021",
      amount:"300.25",
      transaction_notes:"deposit check#3",
      transaction_type:"deposit",
      completion_status:"complete"},
    { transaction_id:"4",
      account_id:"1",
      time:"10:15:21 Sep 1, 2021",
      amount:"400.15",
      transaction_notes:"deposit check#4",
      transaction_type:"deposit",
      completion_status:"complete"},
    { transaction_id:"5",
      account_id:"1",
      time:"10:15:21 Aug 1, 2022",
      amount:"500.50",
      transaction_notes:"deposit check#5",
      transaction_type:"deposit",
      completion_status:"complete"},
    { transaction_id:"6",
      account_id:"1",
      time:"20:35:21 Aug 3, 2022",
      amount:"600.05",
      transaction_notes:"withdraw to buy shoes",
      transaction_type:"withdraw",
      completion_status:"complete"}
  ];

  ngOnInit(): void {
    // this.myTransactionService.getMyTransactions(this.url+this.accountId).subscribe((Res) => {
    //   console.log(Res);
    //   console.log(Res.lisT_DMODEL_Transactions);
    //   console.log(Res.numberOfTransactions);
    //   this.myTransactions = Res.lisT_DMODEL_Transactions;
    // });
        this.myTransactions = this.trans;    
  }
}
// export interface TransactionResponse{
//   NumberOfTransactions: number
//   LIST_DMODEL_Transactions: Array<Transaction>
// }