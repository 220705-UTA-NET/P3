import { Component, ViewChild } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Budget } from '../../services/models/budget';

import { BudgetItemComponent } from './budget-item.component';

let expectedBudget = {budgetId:1,customerId:1,accountId:1,monthlyAmount:100,warningAmount:50,remaining:80,startDate:new Date()};

describe('BudgetItemComponent', () => {
  let component: BudgetItemComponent;
  let fixture: ComponentFixture<BudgetItemComponent>;
  let testHostComponent: TestHostComponent;
  let testHostFixture:ComponentFixture<TestHostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BudgetItemComponent, TestHostComponent ]
    })
    .compileComponents();

    //fixture = TestBed.createComponent(BudgetItemComponent);
    //component = fixture.componentInstance;
    //fixture.detectChanges();
    testHostFixture = TestBed.createComponent(TestHostComponent);
    testHostComponent = testHostFixture.componentInstance;
    testHostFixture.detectChanges();
    //component.input = expectedBudget;
    //component.budget = expectedBudget;
  });

  it('should create', () => {
    expect(testHostComponent).toBeTruthy();
  });
  it('should have amount to budget amount',()=>{
     expect(testHostComponent.budgetItemComponent?.monthlyAmount).toBe(expectedBudget.monthlyAmount.toString());
  });
  it('should turn on/off enableEdit',()=>{
    //testHostComponent.budgetItemComponent!.enableEdit = false;
    testHostComponent.budgetItemComponent?.onClickEdit();
    expect(testHostComponent.budgetItemComponent?.enableEdit).toBeTrue();
    testHostComponent.budgetItemComponent?.onClickEdit();
    expect(testHostComponent.budgetItemComponent?.enableEdit).toBeFalse();
  });
  it('should change amount to 200',()=>{
    testHostComponent.budgetItemComponent!.monthlyAmount = "200";
    testHostComponent.budgetItemComponent!.onClickSave();
    expect(testHostComponent.budget!.monthlyAmount).toBe(200);
  })
  it('should have 100% budget',()=>{
    testHostComponent.budget!.remaining = -1;
    testHostComponent.budgetItemComponent!.ngOnInit();//i guess I have to force ngOnInit to have it change
    expect(testHostComponent.budgetItemComponent!.percentS).toBe("100%");
  })
  it('should delete which is checked via test host component true statement',()=>{
    testHostComponent.budgetItemComponent!.onClickDelete();
    expect(testHostComponent.budget).toBe(null);
  })
  @Component({
    template:`
    <app-budget-item
    [budget]="budget"
    (editBudgetEvent)="onEmitChanges(budget)"
    (deleteBudgetEvent)="onEmitDelete(budget)"
    >   
    </app-budget-item> `
  })
  class TestHostComponent{
    budget:Budget|null = expectedBudget;
    @ViewChild(BudgetItemComponent)
    budgetItemComponent:BudgetItemComponent|undefined;
    onEmitDelete(budget:Budget){
      this.budget = null;
    }
  }
});
