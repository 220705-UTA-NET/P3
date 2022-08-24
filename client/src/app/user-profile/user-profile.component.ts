import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
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
  template: `
    <input #customerID
      (keyup.enter)="getCustomer(customerID.value)"
      (blur)="getCustomer(customerID.value); customerID.value='' ">

    <button type="button" (click)="getCustomer(customerID.value)">Submit</button>

    <p>{{name}}</p>
    <p>{{email}}</p>
    <p>{{phone}}</p>
   `
})
export class UserProfileComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  name = '';
  email = '';
  phone = '';
  getCustomer(value: number) {
    /* this.http.get(`https://localhost:4000/customer/${value}`, {
         observe: "response",
         responseType: "json"
       }).subscribe((result) => {
         const resultBody: any = result.body;
         this.name = resultBody.name;
         this.email = resultBody.email;
         this.phone = resultBody.phone;
       })
    */
  });
    
  }

}
