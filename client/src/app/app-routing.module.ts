import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatboxComponent } from './chatbox/chatbox.component';
import { AccountComponent } from './account/account.component';
import { HeaderComponent } from './header/header.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { BudgetListComponent } from './budget-calculator/budget-list/budget-list.component';
import { AddBudgetComponent } from './budget-calculator/add-budget/add-budget.component';
import { LoginComponent } from './Copper_Components/login/login.component';
//import { RegisterComponent } from './Copper_Components/register/register.component';

const routes: Routes = [
  {path:'', component:HeaderComponent},//Possible Breakpoint
  {path:'budget', component:BudgetListComponent},
  {path:'budget/addnew', component:AddBudgetComponent},
  {path: "chatbox", component: ChatboxComponent},
  //{ path: 'customer', component: UserProfileComponent},
  { path: 'accounts', component: UserProfileComponent},
  { path: '' , component:  HeaderComponent},
 // { path: 'register', component:RegisterComponent},
  { path: 'login', component:LoginComponent}

]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }