import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BudgetService } from '../../services/budget.service';
import { Budget } from '../../services/models/budget';

@Component({
  selector: 'app-add-budget',
  templateUrl: './add-budget.component.html',
  styleUrls: ['../../app.component.css']
})
export class AddBudgetComponent implements OnInit {
  monthlyAmount:number = 0;
  warningAmount:number = 0;
  constructor(private budgetService: BudgetService, private router:Router) { }

  ngOnInit(): void {
  }

  onClickSubmit(){
    let mydate = new Date;
    mydate.setDate(1);//set to the first of this month | need to change component to allow user to set first day
    mydate.setHours(0,0,0);//set to the first hour
    //mydate.setUTCHours(0,0,0);
    let budget:Budget = {budgetId:this.budgetService.getNextId(),customerId:1,accountId:1,monthlyAmount:this.monthlyAmount,warningAmount:this.warningAmount, remaining:this.monthlyAmount, startDate:mydate};
    console.log(budget);
    this.budgetService.addNewBudget(budget).subscribe((res)=>{
      console.log(res)
    });
    this.router.navigate(['budget']);
  }
}
