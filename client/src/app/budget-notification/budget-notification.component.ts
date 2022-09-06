import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { WARNING } from '../budget-warning';
import { map, pipe } from 'rxjs';
import { BudgetNotifyService } from '../budget-notify.service';

@Component({
  selector: 'app-budget-notification',
  templateUrl: './budget-notification.component.html',
  styleUrls: ['./budget-notification.component.css'],
})
export class BudgetNotificationComponent implements OnInit {
  @Output() noteOpen = new EventEmitter<boolean>();
  @Input() childAccounts: WARNING[] = [];

  constructor(
    private http: HttpClient,
    private budgetService: BudgetNotifyService
  ) {}

  ngOnInit(): void {
    console.log('child accounts:');
    console.log(this.childAccounts);
  }

  closeNotification() {
    this.noteOpen.emit(false);
  }
}
