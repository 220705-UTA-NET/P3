import { Checking } from './../checking-account/checking-account.component';
import { Component } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog'
import { UserProfileDialogComponent } from '../user-profile-dialog/user-profile-dialog.component';

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

  customerSet: Boolean = false;
  checking: Checking[] = [];
  constructor(private http: HttpClient, private dialog: MatDialog) { }

  customer: Customer = {
    id: 0,
    firstname: "",
    lastname: "",
    email: "",
    phoneNumber: "",
    password: ""
  };

  
  getCustomer(value: any) {
    const headers = {'Access-Control-Allow-Origin' : '*'};
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

  openProfileDialog(){
    const profileDialogConfig = new MatDialogConfig();

    profileDialogConfig.autoFocus = true;
    
    profileDialogConfig.data = {
      id: 1,
      title: 'Profile',
      data: {
        fName : this.customer.firstname,
        lName : this.customer.lastname,
        email : this.customer.email,
        phoneNumber : this.customer.phoneNumber
      }
    }

    const obj = this.dialog.open(UserProfileDialogComponent, profileDialogConfig);

    obj.afterClosed().subscribe(
      data => {
        if(typeof(data.fName) !== "undefined"){
          this.customer.firstname = data.fName;
          this.customer.lastname = data.lName;
          this.customer.email = data.email;
          this.customer.phoneNumber = data.phoneNumber;
          this.submitChanges();
        }
        
      }
    )
  }

  // editFirstName(event: any) {
  //   this.customer.firstname = event.target.value
  // }

  // editLastName(event: any) {
  //   this.customer.lastname = event.target.value
  // }

  // editEmail(event: any) {
  //   this.customer.email = event.target.value
  // }

  // editPhoneNumber(event: any) {
  //   this.customer.phoneNumber = event.target.value
  // }

  // editPassword(event: any) {
  //   this.customer.password = event.target.value
  // }

  submitChanges() {
    console.log(this.customer);
    const headers = new HttpHeaders({ 'Content-Type' : 'application/json', 'Accept' : 'application/json'});
    this.http.put('https://localhost:7249/userprofile', this.customer, {headers}).subscribe()
  }
    
}
