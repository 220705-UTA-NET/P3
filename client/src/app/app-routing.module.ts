import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatboxComponent } from './chatbox/chatbox.component';

const routes: Routes = [
  {path: "chatbox", component: ChatboxComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
