import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AppComponent } from './app.component';
import { UserProfileComponent } from './user-profile/user-profile.component';


const routes: Routes = [{ path: '\customer', component: UserProfileComponent},
                        { path: '\accounts', component: AccountComponent },
                        { path: '\\' , component: AppComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
