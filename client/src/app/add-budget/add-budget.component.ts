import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BudgetService } from '../services/budget.service';
import { Budget } from '../services/models/budget';

@Component({
  selector: 'app-add-budget',
  templateUrl: './add-budget.component.html',
  styleUrls: ['../app.component.css']
})
export class AddBudgetComponent implements OnInit {
  monthlyAmount:number = 0;
  warningAmount:number = 0;
  //enableEdit = false;
  constructor(private budgetService: BudgetService, private router:Router) { }

  ngOnInit(): void {
  }

  onClickSubmit(){
    let mydt = new Date;
    let mydt1 = mydt.toLocaleDateString();
    console.log(mydt1);
    //mydt1.toString("MM/dd/yyyy h:mm:ss tt");
    let budget:Budget = {budgetId:this.budgetService.getNextId(),customerId:1,accountId:1,monthlyAmount:this.monthlyAmount,warningAmount:this.warningAmount, remaining:this.monthlyAmount, startDate:mydt1};
    console.log(budget);
    this.budgetService.addNewBudget(budget).subscribe((res)=>{
      console.log(res)
    });
    this.router.navigate(['budget']);
  }
}
