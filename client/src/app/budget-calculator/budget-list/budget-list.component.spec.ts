import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { BehaviorSubject, of } from 'rxjs';
import { BudgetItemComponent } from '../budget-item/budget-item.component';
import { BudgetService } from '../../services/budget.service';
import { Budget } from '../../services/models/budget';
import { BudgetListComponent } from './budget-list.component';

let expectedBudget = {budgetId:1,customerId:1,accountId:1,monthlyAmount:100,warningAmount:50,remaining:80,startDate:new Date()};

describe('BudgetListComponent', () => {
  let mockRouter = {
    navigate: jasmine.createSpy('navigate')
  }
  const response = JSON.parse('{"status":200}');
  let budgetServiceStub : Partial<BudgetService>;
  budgetServiceStub = {
    addNewBudget(budget) {
      return of(response);
    },
    getNextId(){
      return 1;
    },
    updateLength(){
      return;
    },
    getBudgetList(id:number){
      return new BehaviorSubject<[Budget]>([expectedBudget]);
    },
    updateBudget(budget) {
      return of(response);
    },
    deleteBudget(budget){
      return of(response);
    }
  }
  let component: BudgetListComponent;
  let fixture: ComponentFixture<BudgetListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BudgetListComponent, FakeChild ],
      providers: [{provide: BudgetService, useValue: budgetServiceStub},
                  {provide: Router, useValue: mockRouter}
      ]
    })
    .compileComponents();
    expectedBudget = {budgetId:1,customerId:1,accountId:1,monthlyAmount:100,warningAmount:50,remaining:80,startDate:new Date()};

    fixture = TestBed.createComponent(BudgetListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should get list',()=>{
    component.ngOnInit();
    expect(component.budgetList).toEqual([expectedBudget]);
  });
  it('should get call onEmitChanges',()=>{
    const childElement = fixture.debugElement.query(By.directive(FakeChild));
    const fakeChildComponent:FakeChild = childElement.componentInstance;
    spyOn(window,'alert');
    fakeChildComponent.editBudgetEvent.emit(expectedBudget);
    expect(window.alert).toHaveBeenCalledWith("successfully updated bugdet");
  });
  it('should call onEmitDelete',()=>{
    const childElement = fixture.debugElement.query(By.directive(FakeChild));
    const fakeChildComponent:FakeChild = childElement.componentInstance;
    spyOn(component,'ngOnInit');
    fakeChildComponent.deleteBudgetEvent.emit(expectedBudget);
    expect(component.ngOnInit).toHaveBeenCalled();
  });
  it('should call router',()=>{
    component.onAddNew();
  });
  @Component({
    selector: 'app-budget-item',
    template: ``,
  })
  class FakeChild implements Partial<BudgetItemComponent>{
    @Input() budget: Budget = expectedBudget;
    @Output() editBudgetEvent : EventEmitter<Budget> = new EventEmitter(); 
    @Output() deleteBudgetEvent: EventEmitter<Budget> = new EventEmitter(); 
  }
});
