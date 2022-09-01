import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


export interface Customer {
  id: number,
  firstName: string,
  lastName: string,
  email: string,
  phoneNumber: string,
  password: string
}

@Component({
  selector: 'app-user-password-dialog',
  templateUrl: './user-password-dialog.component.html',
  styleUrls: ['./user-password-dialog.component.css']
})

export class UserPasswordDialogComponent implements OnInit {
  public form : FormGroup;
  public fName : string = this.data.data.fName;
  public lName : string = this.data.data.lName;
  public email : string = this.data.data.email;
  public phoneNumber : string = this.data.data.phoneNumber;


  constructor(private fb : FormBuilder,
              private dialogRef: MatDialogRef<UserPasswordDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data : any) {
                this.form = this.fb.group({
                  fName : this.fName,
                  lName : this.lName,
                  email : this.email,
                  phoneNumber: this.phoneNumber

                })
              }

  ngOnInit(): void {
    this.form = this.fb.group({
      fName : this.fName,
      lName : this.lName,
      email : this.email,
      phoneNumber: this.phoneNumber
    });
  }

  save() {
    this.dialogRef.close(this.form.value);
  }

  close() {
    this.dialogRef.close({});
  }

}

