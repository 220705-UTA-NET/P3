import { Component } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

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
  phone = 0;

  getCustomer(value: any) {
    this.http.get(`https://localhost:7249/userprofile`, {
         params: new HttpParams().set('customerId', value),
         observe: "response",
         responseType: "json"
    }).subscribe((result) => {
         console.log(result);
         const resultBody: any = result.body;
         this.name = resultBody.firstname + " " + resultBody.lastname;
         this.email = resultBody.email;
         this.phone = resultBody.phoneNumber;
       })
    
    console.log("In getCustomer: " + value);
  }
    
}
