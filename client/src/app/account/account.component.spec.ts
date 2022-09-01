import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AccountComponent } from './account.component';

describe('AccountComponent', () => {
  let component: AccountComponent;
  let fixture: ComponentFixture<AccountComponent>;
  let controller: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [AccountComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(AccountComponent);
    component = fixture.componentInstance;
    controller = TestBed.inject(HttpTestingController);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should make get request', () => {
    component.getAccounts(1);

    const request = controller.expectOne("https://localhost:7249/accounts?customerId=1");

    const accounts = [{
      accountId: 1,
      type: 0,
      balance: 10000,
      customerId: 1
    }]
    request.flush([{ "accountId": 1, "type": 0, "balance": 10000, "customerId": 1 }])

    expect(component.accounts).toEqual(accounts);
  });
});
