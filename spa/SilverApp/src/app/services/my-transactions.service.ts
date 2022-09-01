import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Transaction } from "../Models/transactions";
import { Observable } from "rxjs";
@Injectable({
  providedIn: 'root'
})
export class MyTransactionsService {

  constructor(private http:HttpClient) { }
  getMyTransactions(url:string): Observable<Transaction[]>{
    return this.http.get<Transaction[]>(url);
  }
}
