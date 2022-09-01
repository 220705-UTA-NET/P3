import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SendMoneyComponent } from './send-money/send-money.component';
import { RequestMoneyComponent } from './request-money/request-money.component';
import { MyTransactionsComponent } from './my-transactions/my-transactions.component';
import { DepositComponent } from './deposit/deposit.component';
import { WithdrawalComponent } from './withdrawal/withdrawal.component';

const routes: Routes = [
  {path: 'My-Transactions', component: MyTransactionsComponent},
  {path: 'Deposit', component: DepositComponent},
  {path: 'Withdrawal', component: WithdrawalComponent},
  {path: 'Send-Money', component: SendMoneyComponent},
  {path: 'Request-Money', component: RequestMoneyComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
