
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { BudgetService } from '../../services/budget.service';

import { AddBudgetComponent } from './add-budget.component';

describe('AddBudgetComponent', () => {
  let mockRouter = {
    navigate: jasmine.createSpy('navigate')
  }
  let component: AddBudgetComponent;
  let fixture: ComponentFixture<AddBudgetComponent>;
  let budgetServiceStub: Partial<BudgetService>;
  const response = JSON.parse('{"status":"200"}');
  budgetServiceStub = {
    addNewBudget(budget) {
      return of(response);
    },
    getNextId(){
      return 1;
    }
  }
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddBudgetComponent ],
      providers:[{provide:BudgetService, useValue:budgetServiceStub},
                  {provide: Router, useValue: mockRouter}
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddBudgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should fake add budget', ()=>{
    component.onClickSubmit();
    expect(component).toBeTruthy();
  })
});
