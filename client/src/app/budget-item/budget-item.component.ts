import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Budget } from '../services/models/budget';

@Component({
  selector: 'app-budget-item',
  templateUrl: './budget-item.component.html',
  styleUrls: ['../app.component.css','./budget-item.component.css']
})
export class BudgetItemComponent implements OnInit {
@Input() budget!:Budget;
@Output() editBudgetEvent : EventEmitter<Budget> = new EventEmitter(); 
@Output() deleteBudgetEvent: EventEmitter<Budget> = new EventEmitter();
enableEdit = false;
monthlyAmount:string = "";
warningLimit:string = "";
percentS:string = "";
colorS = "lightgreen";
  constructor(private router:Router) { }

  ngOnInit(): void {
    this.monthlyAmount = this.budget.monthlyAmount.toString();
    this.warningLimit = this.budget.warningAmount.toString();
    let percent = 50;
    if(this.budget.remaining < 0){
      percent = 100;
      this.percentS = percent + "%";
    }
    else{
      percent = 100 - ((this.budget.remaining / this.budget.monthlyAmount)*100); 
      this.percentS = percent + "%";
    }

    //percent = 88;
    if(percent < 30){
      this.colorS = "limegreen";
    }
    if(percent >=30 && percent < 80){
      this.colorS = "#fff3cd";
    }
    if(percent >= 80){
      this.colorS = "#f8d7da";
    }
    
    console.log(this.percentS);
  }
  onClickEdit(){
    this.enableEdit = !this.enableEdit;
  }
  onClickSave(){
    this.budget.warningAmount = parseFloat(this.warningLimit);
    this.budget.monthlyAmount = parseFloat(this.monthlyAmount);
    this.editBudgetEvent.emit(this.budget);
    this.onClickEdit();
  }
  onClickDelete(){
    this.deleteBudgetEvent.emit(this.budget);
  }
}
