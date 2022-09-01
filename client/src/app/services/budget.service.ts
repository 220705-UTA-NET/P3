import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Budget } from './models/budget';

/*const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};*/


let headers = new HttpHeaders();
headers = headers.set('Content-Type', 'application/json');
//headers = headers.set('Authorization', `Bearer ${this.token}`); if we are using jwt Token


@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  private arrayLength = 0;
  //private apiUrl = "http://localhost:5000/tasks";
  /* 
GET
​/Budget​/CustomerBudgets​/{customerId}
POST
​/Budget​/InsertBudget
PUT
​/Budget​/UpdateBudget
DELETE
​/Budget​/DeleteBudget​/{budgetId}
  */
  private apiURL1 = "https://localhost:7249/Budget/";
  private apiUrl = "https://localhost:7249/Budget/CustomerBudgets/";
  constructor(private http:HttpClient) { }
  getBudgetList(customerId:number): Observable<Budget[]>{
    return this.http.get<Budget[]>(this.apiUrl+customerId);
  }
  deleteBudget(budget:Budget){
    return this.http.delete(this.apiURL1+'DeleteBudget/'+budget.budgetId,{observe:'response'});
  }
  updateBudget(budget:Budget){
    return this.http.put(this.apiURL1+'UpdateBudget',budget,{observe:'response'});
  }
  addNewBudget(budget:Budget){
    return this.http.post(this.apiURL1+"InsertBudget", budget,{observe:'response'});
  }
  updateLength(len:number){
    this.arrayLength = len+1;
  }
  getNextId(){
    return this.arrayLength;
  }
}
