import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../../shared/user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user!: User;

  constructor() { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?:NgForm)
  {
    if(form !=null)
    form.reset();
    this.user= {
      first_name:'',
      last_name:'',
      phone: '',
      email: '',
      password: ''

    }
  }

}
