import { Component, OnDestroy, OnInit, Input, Output, EventEmitter} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { isNgTemplate } from '@angular/compiler';

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  styleUrls: ['./deposit.component.css']
})
export class DepositComponent implements OnInit {
  response: any;

  @Input() accountNumber! : number;
  @Output() newTransaction = new EventEmitter<number>();
  @Output() updateBalance = new EventEmitter<number>();

  depositForm = this.formBuilder.group({
    amount: ''
  });

  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }
  ngOnInit(): void {
  }

  onSubmit(): void {
    let formData = this.depositForm.value;
    let accountId = this.accountNumber;
    let amount = formData['amount'];

    let headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Accept': 'application/json' });

    if (accountId != null && amount != null) {
      let args = new HttpParams().set('accountId', accountId).set('amount', amount);
      let url = "https://localhost:7249/API/Transactions/Deposit"; //make sure we have the correct URL here.
      console.log("Data for posting = " + args.toString());
      console.log("Will post to: " + url);

      this.http.post(url, {accountId: this.accountNumber, changeAmount: amount}, {
        headers: headers
      }).subscribe((result: any) => {
        console.log(result);
        // this.response = result;
      })
    }

    //this.depositForm.reset();
    this.newTransaction.emit();
    this.updateBalance.emit(Number(amount));
  }

}


