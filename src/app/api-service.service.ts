import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from './Customer';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {

  private apiUrl = 'https://localhost:7249/api'

  constructor(private http: HttpClient) { }

  // GET request for customer based on ID.
  getCustomerWithId(id: string): Observable<Customer> {
    const url = `${this.apiUrl}/login?id=${id}`;
    return this.http.get<Customer>(url);
  }

  // Register new customer. Pass in JSON stringified object as parameter.
  registerCustomer(customer: string): Observable<string> {
    const url = `${this.apiUrl}/register`;
    return this.http.post<string>(url, customer, httpOptions);
  }

  //PUT request to reset customer's password.
  resetPassword(password: string): Observable<string> {
    const url = `${this.apiUrl}/resetpassword`;
    return this.http.put<string>(url, password, httpOptions);
  }

}
