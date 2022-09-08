import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BudgetService } from '../../services/budget.service';
import { Budget } from '../../services/models/budget';
@Component({
  selector: 'app-budget-list',
  templateUrl: './budget-list.component.html',
  styleUrls: ['./budget-list.component.css','../../app.component.css']
})
export class BudgetListComponent implements OnInit {
  budgetList : Budget[];
  doneLoading = false;
  constructor(private budgetService: BudgetService, private router:Router) {
    this.budgetList = [];
   }

  ngOnInit(): void {
    this.budgetService.getBudgetList(1).subscribe((response) =>{
      console.log(response);
      this.budgetList = response;
      if (this.budgetList.length != null)
      {
        this.budgetService.updateLength(this.budgetList.length);
      }
      this.doneLoading = true;
    })
  }
  onAddNew(){
    this.router.navigate(['budget/addnew']);
  }
  onEmitChanges(budget:Budget){
    console.log(budget);
    this.budgetService.updateBudget(budget).subscribe((res) =>{
      console.log(res);
      if(res.status == 200){
        alert("successfully updated bugdet");
      }
    })
  }
  onEmitDelete(budget:Budget){
    this.budgetService.deleteBudget(budget).subscribe((res)=>{
      console.log(res);
    });
    this.ngOnInit();
  }
}
