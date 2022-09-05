import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatboxComponent } from './chatbox/chatbox.component';
import { BudgetListComponent } from './budget-calculator/budget-list/budget-list.component';
import { AddBudgetComponent } from './budget-calculator/add-budget/add-budget.component';
import { HeaderComponent } from './header/header.component';
const routes: Routes = [
  {path:'', component:HeaderComponent},
  {path:'budget', component:BudgetListComponent},
  {path:'budget/addnew', component:AddBudgetComponent},
  {path: "chatbox", component: ChatboxComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
