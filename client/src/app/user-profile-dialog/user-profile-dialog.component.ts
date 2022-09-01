import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserProfileComponent } from '../user-profile/user-profile.component';

@Component({
  selector: 'app-user-profile-dialog',
  templateUrl: './user-profile-dialog.component.html',
  styleUrls: ['./user-profile-dialog.component.css']
})

export class UserProfileDialogComponent implements OnInit {
  public form : FormGroup;
  public fName : string = this.data.data.fName;
  public lName : string = this.data.data.lName;
  public email : string = this.data.data.email;
  public phoneNumber : string = this.data.data.phoneNumber;


  constructor(private fb : FormBuilder,
              private dialogRef: MatDialogRef<UserProfileDialogComponent>,
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
