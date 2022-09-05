import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ChatboxComponent } from './chatbox/chatbox.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ChatBoxComponent } from './chat-box/chat-box.component';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { BudgetListComponent } from './budget-calculator/budget-list/budget-list.component';
import { BudgetItemComponent } from './budget-calculator/budget-item/budget-item.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AddBudgetComponent } from './budget-calculator/add-budget/add-budget.component';
import { HeaderComponent } from './header/header.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { LandingComponent } from './landing/landing.component';
import { FooterComponent } from './footer/footer.component';
import { BudgetNotificationComponent } from './budget-notification/budget-notification.component';
import { BudgetNotifyService } from './budget-notify.service';

@NgModule({
  declarations: [
    AppComponent,
    ChatboxComponent,
    ChatBoxComponent,
    AddBudgetComponent,
    BudgetListComponent,
    BudgetItemComponent,
    NavBarComponent,
    HeaderComponent,
    LandingComponent,
    FooterComponent,
    BudgetNotificationComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
  ],
  providers: [BudgetNotifyService],
  bootstrap: [AppComponent],
})
export class AppModule {}
