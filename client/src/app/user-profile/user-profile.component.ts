import { Component } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

export interface Customer {
  id: number,
  firstname: string,
  lastname: string,
  email: string,
  phoneNumber: string,
  password: string
}

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
})
export class UserProfileComponent {

  constructor(private http: HttpClient) { }

  customer: Customer = {
    id: 0,
    firstname: "",
    lastname: "",
    email: "",
    phoneNumber: "",
    password: ""
  };
  customerSet: Boolean = false;

  getCustomer(value: any) {
    this.http.get(`https://localhost:7249/userprofile`, {
         params: new HttpParams().set('customerId', value),
         observe: "response",
         responseType: "json"
    }).subscribe((result: any) => {
      console.log(result);
      this.customer = result.body;
      this.customerSet = true;
       })
    
    console.log("In getCustomer: " + value);
  }

  editFirstName(event: any) {
    this.customer.firstname = event.target.value
  }

  editLastName(event: any) {
    this.customer.lastname = event.target.value
  }

  editEmail(event: any) {
    this.customer.email = event.target.value
  }

  editPhoneNumber(event: any) {
    this.customer.phoneNumber = event.target.value
  }

  editPassword(event: any) {
    this.customer.password = event.target.value
  }

  submitChanges() {
    console.log(this.customer);
    this.http.put('https://localhost:7249/userprofile', this.customer).subscribe()
  }
    
}
