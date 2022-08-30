import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BudgetService } from '../services/budget.service';
import { Budget } from '../services/models/budget';
@Component({
  selector: 'app-budget-list',
  templateUrl: './budget-list.component.html',
  styleUrls: ['./budget-list.component.css','../app.component.css']
})
export class BudgetListComponent implements OnInit {
  budgetList : Budget[];
  constructor(private budgetService: BudgetService, private router:Router) {
    this.budgetList = [];
   }

  ngOnInit(): void {
    this.budgetService.getBudgetList(1).subscribe((response) =>{
      console.log(response);
      this.budgetList = response;
      this.budgetService.updateLength(this.budgetList.length);
    })
  }
  onAddNew(){
    this.router.navigate(['budget/addnew']);
  }
  onEmitChanges(budget:Budget){
    console.log(budget);
    
  }
  onEmitDelete(budget:Budget){
    this.budgetService.deleteBudget(budget).subscribe((res)=>{
      console.log(res);
    });
    this.ngOnInit();
  }
}
