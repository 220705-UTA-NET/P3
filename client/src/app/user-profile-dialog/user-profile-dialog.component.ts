import { Component, Inject, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormControl, FormGroup, FormGroupDirective, ReactiveFormsModule, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-user-profile-dialog',
  templateUrl: './user-profile-dialog.component.html',
  styleUrls: ['./user-profile-dialog.component.css']
})

export class UserProfileDialogComponent implements OnInit {
  // public fName : string = this.data.data.fName;
  public fName : FormControl = new FormControl(this.data.data.fName);
  //public lName : string = this.data.data.lName;
  public lName : FormControl = new FormControl(this.data.data.lName);
  //public email : string = this.data.data.email;
  public email :  FormControl = new FormControl(this.data.data.email, [Validators.email]);
  //public phoneNumber : string = this.data.data.phoneNumber;
  public phoneNumber : FormControl = new FormControl(this.data.data.phoneNumber, [Validators.pattern("^[0-9]*$"), 
                                                     Validators.minLength(10), Validators.maxLength(10)]);
  
  public form : FormGroup;
  
  matcher = new MyErrorStateMatcher();
  public error : boolean = false;
  public errorMessage : string = "";


  constructor(private fb : FormBuilder,
              private dialogRef: MatDialogRef<UserProfileDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data : any) {
                this.form = this.fb.group({
                  fName: this.fName, 
                  lName: this.lName, 
                  email: this.email,
                  phoneNumber: this.phoneNumber
                })
              }

  ngOnInit(): void {
    this.form = this.fb.group({
      fName: this.fName, 
      lName: this.lName, 
      email: this.email,
      phoneNumber: this.phoneNumber
    });
  }

  save() {
    if(!this.matcher.isErrorState(this.phoneNumber, null) && !this.matcher.isErrorState(this.email, null)){
      this.dialogRef.close(this.form.value);
    }else{
      this.error = true;
      this.errorMessage = "Invalid Information";
    }
  }

  close() {
    this.dialogRef.close({});
  }

}
