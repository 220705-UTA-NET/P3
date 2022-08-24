import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface Customer {
  id: number,
  name: string,
  email: string,
  phone: number
}

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
})
export class UserProfileComponent {

  constructor(private http: HttpClient) { }

  name = 'name';
  email = 'email';
  phone = 'phone';
  getCustomer(value: any) {
    /* this.http.get(`localhost:4000/customer/${value}`, {
         observe: "response",
         responseType: "json"
       }).subscribe((result) => {
         const resultBody: any = result.body;
         this.name = resultBody.name;
         this.email = resultBody.email;
         this.phone = resultBody.phone;
       })
    */
    console.log("In getCustomer: " + value);
  }
    
}
