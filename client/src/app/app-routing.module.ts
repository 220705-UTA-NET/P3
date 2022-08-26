import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatboxComponent } from './chatbox/chatbox.component';
import { ChatBoxComponent } from './chat-box/chat-box.component';

const routes: Routes = [
  {path: "chatbox", component: ChatboxComponent},
  {path: "chatboxtest", component: ChatBoxComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
