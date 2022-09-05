import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
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
import { BudgetListComponent } from './budget-calculator/budget-list/budget-list.component';
import { BudgetItemComponent } from './budget-calculator/budget-item/budget-item.component';
import { AddBudgetComponent } from './budget-calculator/add-budget/add-budget.component';
import { HeaderComponent } from './header/header.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { LandingComponent } from './landing/landing.component';
import { FooterComponent } from './footer/footer.component';
import { UserPasswordDialogComponent } from './user-password-dialog/user-password-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    UserProfileComponent,
    UserProfileDialogComponent,
    AccountComponent,
    ChatboxComponent,
    ChatBoxComponent,
    AddBudgetComponent,
    BudgetListComponent,
    BudgetItemComponent,
    NavBarComponent,
    HeaderComponent,
    LandingComponent,
    FooterComponent,
    UserPasswordDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    MaterialModule,
    BrowserAnimationsModule
  ],
  providers: [
    {provide: MatDialogRef, useValue: {}},
    {provide: MAT_DIALOG_DATA, useValue: {}}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
