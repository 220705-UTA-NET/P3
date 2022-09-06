import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ChatboxComponent } from './chatbox/chatbox.component';
// import { ChatBoxComponent } from './chat-box/chat-box.component';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { BudgetListComponent } from './budget-calculator/budget-list/budget-list.component';
import { BudgetItemComponent } from './budget-calculator/budget-item/budget-item.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClientModule } from '@angular/common/http';
import { AddBudgetComponent } from './budget-calculator/add-budget/add-budget.component';
import { HeaderComponent } from './header/header.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { LandingComponent } from './landing/landing.component';
import { FooterComponent } from './footer/footer.component';
import { ChatService } from './services/chat.service';
import { LoginComponent } from './Copper_Components/login/login.component';
import { RegisterComponent } from './Copper_Components/register/register.component';
import { AuthModule } from '@auth0/auth0-angular';
import { BudgetNotificationComponent } from './budget-notification/budget-notification.component';
import { BudgetNotifyService } from './budget-notify.service';

@NgModule({
  declarations: [
    AppComponent,

    ChatboxComponent,
    // ChatBoxComponent,
    AddBudgetComponent,
    BudgetListComponent,
    BudgetItemComponent,
    NavBarComponent,
    HeaderComponent,
    LandingComponent,
    FooterComponent,
    LoginComponent,
    RegisterComponent,
    BudgetNotificationComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    HttpClientTestingModule,
    AuthModule.forRoot({
      domain: 'dev-ti49ksgx.us.auth0.com',
      clientId: 'wFseXaNCNWXgvAxWNYecaiZCjTIL5N1C'
    })
  ],
  providers: [BudgetNotifyService, ChatService],
  bootstrap: [AppComponent]
})
export class AppModule {}

