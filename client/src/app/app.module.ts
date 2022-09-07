import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserProfileComponent} from './user-profile/user-profile.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from './material/material.module';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserProfileDialogComponent } from './user-profile-dialog/user-profile-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccountComponent } from './account/account.component';
import { ChatboxComponent } from './chatbox/chatbox.component';

 import { ChatBoxComponent } from './chat-box/chat-box.component';
import { FormsModule } from '@angular/forms';
import { BudgetListComponent } from './budget-calculator/budget-list/budget-list.component';
import { BudgetItemComponent } from './budget-calculator/budget-item/budget-item.component';

import { AddBudgetComponent } from './budget-calculator/add-budget/add-budget.component';
import { HeaderComponent } from './header/header.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { LandingComponent } from './landing/landing.component';
import { FooterComponent } from './footer/footer.component';

import { UserPasswordDialogComponent } from './user-password-dialog/user-password-dialog.component';
import { TopComponent } from './top/top.component';
import { SendMoneyComponent } from './send-money/send-money.component';
import { RequestMoneyComponent } from './request-money/request-money.component';
import { MyTransactionsComponent } from './my-transactions/my-transactions.component';
import { DepositComponent } from './deposit/deposit.component';
import { WithdrawalComponent } from './withdrawal/withdrawal.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { LoginComponent } from './Copper_Components/login/login.component';
import { RegisterComponent } from './Copper_Components/register/register.component';

import { ChatService } from './services/chat.service';
import { LoginComponent } from './Copper_Components/login/login.component';
import { RegisterComponent } from './Copper_Components/register/register.component';
import { AuthModule } from '@auth0/auth0-angular';
import { BudgetNotificationComponent } from './budget-notification/budget-notification.component';
import { BudgetNotifyService } from './budget-notify.service';
import { AuthInterceptor } from './services/auth.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    UserProfileComponent,
    UserProfileDialogComponent,
    AccountComponent,
    ChatboxComponent,
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
    MyTransactionsComponent,
    DepositComponent,
    WithdrawalComponent,
    LoginComponent,
    RegisterComponent,
    BudgetNotificationComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    MaterialModule,
    BrowserAnimationsModule,
    NgxPaginationModule,
    HttpClientTestingModule,
    AuthModule.forRoot({
      domain: 'dev-ti49ksgx.us.auth0.com',
      clientId: 'wFseXaNCNWXgvAxWNYecaiZCjTIL5N1C'
    })
  ],
  providers: [{provide:HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: MatDialogRef, useValue: {}},
    {provide: MAT_DIALOG_DATA, useValue: {}},
    BudgetNotifyService, ChatService],

  bootstrap: [AppComponent]
})
export class AppModule {}

