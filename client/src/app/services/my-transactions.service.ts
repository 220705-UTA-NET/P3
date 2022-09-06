import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Transaction } from "../models/transactions";
import { Observable } from "rxjs";
@Injectable({
  providedIn: 'root'
})
export class MyTransactionsService {

  // constructor(private http:HttpClient) { }
  // getMyTransactions(url:string): Observable<Transaction[]>{
  //   return this.http.get<Transaction[]>(url);
  // }

  constructor(private http:HttpClient) { }
getMyTransactions(url:string): Observable<TransactionResponse>{
    console.log("Ran GET request");
    console. log(this.http.get<TransactionResponse>(url));
    return this.http.get<TransactionResponse>(url);
  }
}

//instead of Transaction[] - list of transaction, we're using TransactionResponse with the list as property
export interface TransactionResponse{
  numberOfTransactions: number
  lisT_DMODEL_Transactions: Transaction[]
}