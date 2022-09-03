import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  styleUrls: ['./deposit.component.css']
})
export class DepositComponent implements OnInit {
  var postResult: String;

  constructor() { } constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  // Hau: modify above methods as required (look at search-book.comp.ts)

  // add onSubmit() {
  //   let accountId = formData['xyz'];
  //   let changeAmount = formData['xyz'];

  //   console.log(accountId);
  //   console.log(changeAmount);
  //   let headers = new HttpHeaders({ 'Content-Type' : 'application/json', 'Accept' : 'application/json'});
  //   let args = new HttpParams().set('accountId', accountId).set('changeAmount', changeAmount).set('accountType', 0);
  //   this.http.post( `https://localhost:7249/API/Transactions/Deposit?` + args.toString(), {
  //     headers: headers
  //   }).subscribe((result : any) => {
  //     console.log(result);
  //     this.postResult = result;
  //   })


  // }
}
