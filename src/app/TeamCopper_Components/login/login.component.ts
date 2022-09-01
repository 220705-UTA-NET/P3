import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { User } from '../../shared/user.model';
import { ApiServiceService } from 'src/app/api-service.service';
import { Customer } from 'src/app/Customer';
import  { AccessToken } from 'src/app/Customer';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user!: User;
  customer!: Customer;


  constructor(private router: Router, private http: HttpClient, private ApiService: ApiServiceService) {
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
  }

  accessToken: string = '';


  // On form submission, onSubmit() function gets values from email and password input fields
  onSubmit() {
    this.user.login = (<HTMLInputElement>document.getElementById("email")).value;
    this.user.password = (<HTMLInputElement>document.getElementById("password")).value;
    console.log(this.user.login);
    console.log(this.user.password);

    this.ApiService.getCustomerWithId(this.user.login).subscribe(customer => { 
      this.customer = customer
      console.log(this.customer.firstName)
    });
    
  }

  resetForm(form?:NgForm)
  {
    if(form !=null)
    form.reset();
    this.user= {
      login: '',
      password: ''
    }
  }

}
