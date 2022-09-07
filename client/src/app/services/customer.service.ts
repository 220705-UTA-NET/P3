import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.authorization';
import { AccessToken } from '../models/customer.authorization';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  public apiUrl = 'https://localhost:7249/login/customer';
  private data: any = [];

  constructor(private http: HttpClient) { }

  getAccessToken() {
    this.http.get(this.apiUrl).subscribe((res) => {
      this.data = res;
      console.log(this.data);

      localStorage.setItem("access-token", this.data);
    })
  };

  postLogin(authCredentials: string) {
    const response = this.http.get(this.apiUrl, {
      headers: {'Authorization': authCredentials},
      observe: "response",
      responseType: "json"
    });

    return response;
  }

  // GET request for customer based on ID.
  getCustomerWithId(id: string): Observable<Customer> {
    const url = `${this.apiUrl}/login?id=${id}`;
    return this.http.get<Customer>(url);
  }

  // Register new customer. Pass in JSON stringified object as parameter.
  
  registerCustomer(customer: string): Observable<string> {
    const url = `${this.apiUrl}/Register`;
    return this.http.post<string>(url, customer, httpOptions);
  }

  //PUT request to reset customer's password.
  resetPassword(password: string): Observable<string> {
    const url = `${this.apiUrl}/resetpassword`;
    return this.http.put<string>(url, password, httpOptions);
  }

}
