import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CheckingAccountComponent } from './checking-account/checking-account.component';
import { UserProfileComponent } from './user-profile/user-profile.component';

const routes: Routes = [{ path: "\customer", component: UserProfileComponent, },
                        { path: "\accounts", component: CheckingAccountComponent, }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
