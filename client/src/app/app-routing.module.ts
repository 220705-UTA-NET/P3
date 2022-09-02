import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatboxComponent } from './chatbox/chatbox.component';
import { BudgetListComponent } from './budget-list/budget-list.component';
import { AddBudgetComponent } from './add-budget/add-budget.component';
const routes: Routes = [
  {path:'', component:BudgetListComponent},
  {path:'budget', component:BudgetListComponent},
  {path:'budget/addnew', component:AddBudgetComponent},
  {path: "chatbox", component: ChatboxComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }