import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { CheckingAccountComponent } from './checking-account/checking-account.component';

const routes: Routes = [{ path: "\customer", component: UserProfileComponent},
{path: "\accounts", component: CheckingAccountComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
