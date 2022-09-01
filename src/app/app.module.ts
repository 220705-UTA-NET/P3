import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './TeamCopper_Components/register/register.component';
import { LoginComponent } from './TeamCopper_Components/login/login.component';
import { ResetComponent } from './TeamCopper_Components/reset/reset.component';

import { AuthModule } from '@auth0/auth0-angular';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    ResetComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    AuthModule.forRoot({
      domain: 'dev-ti49ksgx.us.auth0.com',
      clientId: 'wFseXaNCNWXgvAxWNYecaiZCjTIL5N1C'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
