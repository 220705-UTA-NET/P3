import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BudgetListComponent } from './budget-calculator/budget-list/budget-list.component';
import { AddBudgetComponent } from './budget-calculator/add-budget/add-budget.component';
import { LandingComponent } from './landing/landing.component';

const routes: Routes = [
  { path: 'budget-calculator', component: BudgetListComponent },
  { path: '', component: LandingComponent },
  // { path: '', component: BudgetListComponent },
  // {path:'budget', component:BudgetListComponent},
  {path:'budget/addnew', component:AddBudgetComponent}
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }