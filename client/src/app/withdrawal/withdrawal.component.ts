import { Component, OnDestroy, OnInit, Input} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-withdrawal',
  templateUrl: './withdrawal.component.html',
  styleUrls: ['./withdrawal.component.css']
})
export class WithdrawalComponent implements OnInit {

  response: any;
  @Input() accountNumber!: number;
//accountId: any; this is not yet available, will need to be integrated with Accounts Team.

withdrawalForm = this.formBuilder.group({
    amount: ''
});

  constructor(private formBuilder: FormBuilder, private http: HttpClient) {
    //we have to populate accountID here.
    //Accounts team will pass the accountID in someway
  
   }

  ngOnInit(): void {
  }
  onSubmit(): void {
    let formData = this.withdrawalForm.value;
    let accountId = this.accountNumber;
    let amount = formData['amount'];

    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json' });
    if (accountId != null && amount != null) {
      let args = new HttpParams().set('accountId', accountId).set('amount', amount);
      let url = "https://localhost:7249/API/Transactions/Withdrawal?"; //make sure we have the correct URL here.
      console.log("Data for posting = " + args.toString());
      console.log("Will post to: " + url);
      this.http.post(url + args.toString(), {
        headers: headers
      }).subscribe((result: any) => {
        console.log(result);
        this.response = result;
      })
    }
    this.withdrawalForm.reset();
  }

}


