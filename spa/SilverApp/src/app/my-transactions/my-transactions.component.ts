import { Component, OnInit } from '@angular/core';
import { Transaction } from '../Models/transactions';
import { MyTransactionsService } from '../services/my-transactions.service';
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
  private url:string = ""; //this is the swagger endpoint
  
  constructor(private myTransactionService:MyTransactionsService) { 
    //populate the real customerid here after authentication is done.
  }

  ngOnInit(): void {
    this.myTransactionService.getMyTransactions(this.url+this.accountId).subscribe((Res) => {
      this.myTransactions = Res;
    });
  }
}
