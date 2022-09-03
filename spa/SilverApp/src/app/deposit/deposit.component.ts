import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  styleUrls: ['./deposit.component.css']
})
export class DepositComponent implements OnInit {

  response: any;

  depositForm = this.formBuilder.group({
    accountId: '',
    amount: ''
  });

  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }
  ngOnInit(): void {
  }
  onSubmit(): void {
    let formData = this.depositForm.value;
    let accountId = formData['accountId'];
    let amount = formData['amount'];

    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
    if (accountId != null && amount != null) {
      let args = new HttpParams().set('accountId', accountId).set('amount', amount);
      let url = "https://localhost:7249/API/Transactions/Deposit?"; //make sure we have the correct URL here.
      console.log("Data for posting = " + args.toString());
      console.log("Will post to: " + url);
      this.http.post(url + args.toString(), {
        headers: headers
      }).subscribe((result: any) => {
        console.log(result);
        this.response = result;
      })
    }
    this.depositForm.reset();
  }

}


