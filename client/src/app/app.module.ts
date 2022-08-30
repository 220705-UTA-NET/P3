$ cat app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { TemplateComponent } from './template/template.component';
import { BudgetListComponent } from './budget-list/budget-list.component';
import { BudgetItemComponent } from './budget-item/budget-item.component';
import { AppRoutingModule } from './app-routing.module';
import {HttpClientModule} from '@angular/common/http';
import { AddBudgetComponent } from './add-budget/add-budget.component';

@NgModule({
  declarations: [
    AppComponent,
    TemplateComponent,
    BudgetListComponent,
    BudgetItemComponent,
    AddBudgetComponent

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
