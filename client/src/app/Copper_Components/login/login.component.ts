import { Component, OnInit, Injectable} from '@angular/core';
import { AuthConfigService, AuthService } from '@auth0/auth0-angular';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { User } from 'src/app/models/user.model';
import { Customer } from 'src/app/models/customer.authorization';
import { AccessToken } from 'src/app/models/customer.authorization';
import { CustomerService } from 'src/app/services/customer.service';
import { ChatboxComponent } from 'src/app/chatbox/chatbox.component';
import { Buffer } from 'buffer';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user!: User;
  customer!: Customer;


  constructor(private router: Router, private CustomerService: CustomerService, public auth: AuthService) {
    // if user does not have login token, re-route them to login
    const checkTokenPresent: AccessToken = JSON.parse(localStorage.getItem("customer") || '{}');
    if (!checkTokenPresent['Access-Token']) {
        this.router.navigate(["/login"]);
    } else {
      this.accessToken = checkTokenPresent['Access-Token']
    }
   }

  ngOnInit() {
    this.resetForm();
    localStorage.clear();
    //this.auth.loginWithRedirect();
  }


  accessToken: string = '';

  loginFailed = false;
  loginResponse: any;


  // On form submission, onSubmit() function gets values from email and password input fields
  onSubmit() {
    this.user.email = (<HTMLInputElement>document.getElementById("email")).value;
    this.user.password = (<HTMLInputElement>document.getElementById("password")).value;
    console.log(this.user.email);
    console.log(this.user.password);

    /*this.ApiService.getCustomerWithId(this.user.login).subscribe(customer => { 
      this.customer = customer
      console.log(this.customer.firstName)
    }); */

    //this.ApiService.getAccessToken();

    // convert username and password into comma separate string. Then to base 64.
    let credentialBase: string = `${this.user.email}:${this.user.password}`;
    let encodedString = Buffer.from(credentialBase).toString("base64");

    let authCredentials = `Basic ${encodedString}`;

    this.sendLoginRequest(authCredentials);
    
  }

  sendLoginRequest(authCredentials: string): void {

    this.CustomerService.postLogin(authCredentials)
    .subscribe((result: any) => {
      this.loginResponse = result;

      if (this.loginResponse.status === 200) {
        const customer: Customer = this.loginResponse.body;

        //save token to local storage
        localStorage.setItem('customer', JSON.stringify(customer));
        //re-route to home page
        console.log("log in success");
        this.CustomerService.checkLoggedIn(true);
        //this.router.navigate(["/"]);
        //chatbox isLoggedIn = true;
      } else {
        // let user know that login failed
        console.log("Login failed");
        this.loginFailed = true;
      }
    })
  }

  resetForm(form?:NgForm)
  {
    if(form !=null)
    form.reset();
    this.user= {
      first_name: '',
      last_name: '',
      username: '',
      email: '',
      password: ''
    }
  }

}
