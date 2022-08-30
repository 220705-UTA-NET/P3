export interface Budget{
    budgetId:number;
    customerId:number;
    accountId:number;
    //ccountType:string;
    monthlyAmount:number;
    warningAmount:number;
    remaining:number;
    startDate:string|null;
}