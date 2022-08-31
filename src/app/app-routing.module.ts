import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './TeamCopper_Components/login/login.component';
import { RegisterComponent } from './TeamCopper_Components/register/register.component';
import { ResetComponent } from './TeamCopper_Components/reset/reset.component';

const routes: Routes = [
  { path: 'register', component:RegisterComponent},
  { path: 'login', component:LoginComponent},
  { path: 'reset', component:ResetComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
