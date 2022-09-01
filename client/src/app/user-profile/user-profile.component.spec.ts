import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserProfileComponent } from './user-profile.component';

describe('UserProfileComponent', () => {
  let component: UserProfileComponent;
  let fixture: ComponentFixture<UserProfileComponent>;
  let controller: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      declarations: [ UserProfileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserProfileComponent);
    component = fixture.componentInstance;
    controller = TestBed.inject(HttpTestingController);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set and edit customer', () => {
    const customer = {
      customer_id: 1,
      first_name: "John",
      last_name: "Doe",
      email: "email@example.com",
      phone: "12345678910",
      password: "pass"
    }

    component.customer = customer;
    expect(component.customer).toBe(customer);

    const obj = {
      target: {
        value: "Jake"
      }
    }

    component.editFirstName(obj);
    expect(component.customer.first_name).toBe("Jake");

    obj.target.value = "Smith";
    component.editLastName(obj);
    expect(component.customer.last_name).toBe("Smith");

    obj.target.value = "newemail@example.com";
    component.editEmail(obj);
    expect(component.customer.email).toBe("newemail@example.com");

    obj.target.value = "99999999999";
    component.editPhoneNumber(obj);
    expect(component.customer.phone).toBe("99999999999");

    obj.target.value = "newpass";
    component.editPassword(obj);
    expect(component.customer.password).toBe("newpass");
  });

  it('should make get request', () => {
    component.getCustomer(1);

    const request = controller.expectOne("https://localhost:7249/userprofile?customerId=1");

    const customer = {
      customer_id: 1,
      first_name: "Derick",
      last_name: "Xie",
      email: "newemail@gmail.com",
      phone: "99999999999",
      password: "changed"
    }
    request.flush({ "id": 1, "firstname": "Derick", "lastname": "Xie", "email": "newemail@gmail.com", "phoneNumber": "99999999999", "password": "changed" })

    expect(component.customer).toEqual(customer);
  });

});
