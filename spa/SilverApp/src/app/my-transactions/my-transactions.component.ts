import { Component, OnInit } from '@angular/core';
import { Transaction } from '../Models/transactions';
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

  myTransactions: Transaction[] = [];
  accountId = 1; //currently hardcoding customerid = 1
  private url:string = "https://localhost:7249/API/Transactions/TransactionHistory?INPUT_AuthToken=1&INPUT_AccountNumber="; 
  //this is the URL to reach the end
  //temp connection string https://localhost:7249/API/Transactions/TransactionHistory?INPUT_AuthToken=1&INPUT_AccountNumber=1
  
  constructor(private myTransactionService:MyTransactionsService) { 
    //populate the real customerid here after authentication is done.
  }

  ngOnInit(): void {
    this.myTransactionService.getMyTransactions(this.url+this.accountId).subscribe((Res) => {
      console.log(Res);
      console.log(Res.lisT_DMODEL_Transactions);
      console.log(Res.numberOfTransactions);
      this.myTransactions = Res.lisT_DMODEL_Transactions;
    });
  }
}

// export interface TransactionResponse{
//   NumberOfTransactions: number
//   LIST_DMODEL_Transactions: Array<Transaction>
// }