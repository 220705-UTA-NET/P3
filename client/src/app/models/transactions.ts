export class Transaction{
    transaction_id:string; 
    account_id:string;
    time:string;
    amount:string;
    transaction_notes:string;
    transaction_type:string;
    completion_status:string;
constructor(transaction_id:string, account_id:string, time:string, amount:string, transaction_notes:string, transaction_type:string, completion_status:string )
{
this.transaction_id = transaction_id;
this.account_id = account_id;
this.time = time;
this.amount = amount;
this.transaction_notes = transaction_notes;
this.transaction_type = transaction_type;
this.completion_status = completion_status;
}
}