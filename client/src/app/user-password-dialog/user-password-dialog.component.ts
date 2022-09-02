import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TitleStrategy } from '@angular/router';


@Component({
  selector: 'app-user-password-dialog',
  templateUrl: './user-password-dialog.component.html',
  styleUrls: ['./user-password-dialog.component.css']
})

export class UserPasswordDialogComponent implements OnInit {
  public form : FormGroup;
  public error : boolean = false;
  public message : string = "";

  private password : string = this.data.data.password;

  constructor(private fb : FormBuilder,
              private dialogRef: MatDialogRef<UserPasswordDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data : any) {
                this.form = this.fb.group({
                  currentPassword : "",
                  currentPassword2 : "",
                  newPassword : ""
                })
              }

  ngOnInit(): void {
    this.form = this.fb.group({
      currentPassword : "",
      currentPassword2 : "",
      newPassword : ""
    });
  }

  change() {
    if(this.form.value.currentPassword !== this.form.value.currentPassword2){
      this.error=true;
      this.message="Password don't match";
    }else{
      if(this.form.value.currentPassword !== this.password){
        this.error = true;
        this.message="Incorrect Password";
      }else{
        this.error = false;
        this.dialogRef.close(this.form.value.newPassword);
      }
    }
  }

  cancel() {
    this.dialogRef.close({});
  }

}

