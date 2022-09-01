import { Checking } from './../checking-account/checking-account.component';
import { Component } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog'
import { UserProfileDialogComponent } from '../user-profile-dialog/user-profile-dialog.component';

export interface Customer {
  customer_id: number,
  first_name: string,
  last_name: string,
  email: string,
  phone: string,
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
    customer_id: 0,
    first_name: "",
    last_name: "",
    email: "",
    phone: "",
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
        fName : this.customer.first_name,
        lName : this.customer.last_name,
        email : this.customer.email,
        phoneNumber : this.customer.phone
      }
    }

    const obj = this.dialog.open(UserProfileDialogComponent, profileDialogConfig);

    obj.afterClosed().subscribe(
      data => {
        if(typeof(data.fName) !== "undefined"){
          this.customer.first_name = data.fName;
          this.customer.last_name = data.lName;
          this.customer.email = data.email;
          this.customer.phone = data.phoneNumber;
          this.submitChanges();
        }
        
      }
    )
  }

  submitChanges() {
    console.log(this.customer);
    const headers = new HttpHeaders({ 'Content-Type' : 'application/json', 'Accept' : 'application/json'});
    this.http.put('https://localhost:7249/userprofile', this.customer, {headers}).subscribe()
  }
}
