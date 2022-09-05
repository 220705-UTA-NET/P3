import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { HeaderComponent } from './header/header.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ChatboxComponent } from './chatbox/chatbox.component';
import { BudgetListComponent } from './budget-calculator/budget-list/budget-list.component';
import { AddBudgetComponent } from './budget-calculator/add-budget/add-budget.component';
const routes: Routes = [
  {path:'', component:BudgetListComponent},
  {path:'budget', component:BudgetListComponent},
  {path:'budget/addnew', component:AddBudgetComponent},
  {path: "chatbox", component: ChatboxComponent},
  { path: 'customer', component: UserProfileComponent},
  { path: 'accounts', component: AccountComponent },
  { path: '' , component:  HeaderComponent}

]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
