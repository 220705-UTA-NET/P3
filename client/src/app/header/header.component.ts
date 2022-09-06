import { Component, OnInit } from '@angular/core';
import { Observable, Subscriber, Subscription } from 'rxjs';
import { BudgetNotifyService } from '../budget-notify.service';
import { WARNING } from '../budget-warning';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  displayNotice?: boolean;
  accounts: WARNING[] = [];
  sub?: Observable<WARNING[]>;

  constructor(private budgetService: BudgetNotifyService) {}

  ngOnInit(): void {
    this.budgetService.makeRequest().subscribe((result) => {
      this.accounts = result;
      console.log(this.accounts);

      if (this.accounts.length > 0) {
        // console.log('hey');
        this.showNotice();
      } else {
        // console.log('no');
        this.closeNotice();
      }
    });
  }

  showNotice() {
    this.displayNotice = true;
  }

  closeNotice() {
    this.displayNotice = false;
  }
}
