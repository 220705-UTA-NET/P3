import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ChatboxComponent } from './chatbox/chatbox.component';
import { ReactiveFormsModule  } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { BudgetListComponent } from './budget-calculator/budget-list/budget-list.component';
import { BudgetItemComponent } from './budget-calculator/budget-item/budget-item.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule} from '@angular/common/http';
import { AddBudgetComponent } from './budget-calculator/add-budget/add-budget.component';
import { HeaderComponent } from './header/header.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { LandingComponent } from './landing/landing.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './Copper_Components/login/login.component';
//import { RegisterComponent } from './Copper_Components/register/register.component';
// WILL BE VERY DISSAPOINTED IN THE PERSON THAT UNCOMMENTS THIS!!! import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UserProfileComponent} from './user-profile/user-profile.component';
// DO NOT USE!!!! import { RouterModule } from '@angular/router';
 import { MaterialModule } from './material/material.module';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserProfileDialogComponent } from './user-profile-dialog/user-profile-dialog.component';
//BREAKING CODE import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccountComponent } from './account/account.component';
import { UserPasswordDialogComponent } from './user-password-dialog/user-password-dialog.component';
import { TopComponent } from './top/top.component';
import { SendMoneyComponent } from './send-money/send-money.component';
import { RequestMoneyComponent } from './request-money/request-money.component';
import { MyTransactionsComponent } from './my-transactions/my-transactions.component';
import { DepositComponent } from './deposit/deposit.component';
import { WithdrawalComponent } from './withdrawal/withdrawal.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { ChatService } from './services/chat.service';
//import { AuthModule } from '@auth0/auth0-angular';
//import { BudgetNotificationComponent } from './budget-notification/budget-notification.component';
//import { BudgetNotifyService } from './budget-notify.service';
//import { AuthInterceptor } from './services/auth.interceptor';


import { ChatboxComponent } from './chatbox/chatbox.component';


@NgModule({
  declarations: [
    AppComponent,
    UserProfileComponent,
    UserProfileDialogComponent,
    AccountComponent,
    AddBudgetComponent,
    BudgetListComponent,
    BudgetItemComponent,
    NavBarComponent,
    HeaderComponent,
    LandingComponent,
    FooterComponent,
    UserPasswordDialogComponent,
    TopComponent,
    SendMoneyComponent,
    RequestMoneyComponent,
    ChatboxComponent,
    MyTransactionsComponent,
    DepositComponent,
    WithdrawalComponent,
    LoginComponent,
//    RegisterComponent,
//    BudgetNotificationComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    // Do-Not-USE!!!! RouterModule,
    MaterialModule,
    // BREAKING CODE BrowserAnimationsModule,
    NgxPaginationModule,
    // PLEASE GOD WHY!!! HttpClientTestingModule,
    // AuthModule.forRoot({
    //   domain: 'dev-ti49ksgx.us.auth0.com',
    //   clientId: 'wFseXaNCNWXgvAxWNYecaiZCjTIL5N1C'
    // })
  ],
   providers: [
  //  {provide:HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: MatDialogRef, useValue: {}},
    {provide: MAT_DIALOG_DATA, useValue: {}},
    //BudgetNotifyService, 
    ChatService
  ],

  bootstrap: [AppComponent]
})
export class AppModule {}

