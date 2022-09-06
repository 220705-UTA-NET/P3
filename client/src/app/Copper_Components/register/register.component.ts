import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';

import { CustomerService } from 'src/app/services/customer.service';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user!: User;

  constructor(private customerservice: CustomerService) { }

  ngOnInit() {
    this.resetForm();
  }

  userCreationData = new FormGroup({
    first_name: new FormControl(''),
    last_name: new FormControl(''),
    username: new FormControl(''),
    password: new FormControl(''),
    phone: new FormControl(''),
    email: new FormControl('')
  });

  createUserAccount(event: any) {
    event.preventDefault();

    const first_name = this.userCreationData.value.first_name;
    const last_name = this.userCreationData.value.last_name;
    const username = this.userCreationData.value.username;
    const password = this.userCreationData.value.password;
    const phone = this.userCreationData.value.phone;
    const email = this.userCreationData.value.email;
  }

  resetForm(form?:NgForm)
  {
    if(form !=null)
    form.reset();
    this.user= {
      first_name:'',
      last_name:'',
      phone: '',
      username: '',
      email: '',
      password: ''

    }
  }
}