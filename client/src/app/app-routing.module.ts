import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { UserProfileComponent } from './user-profile/user-profile.component';


const routes: Routes = [{ path: "\customer", component: UserProfileComponent, },
                        { path: "\accounts", component: AccountComponent, }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
