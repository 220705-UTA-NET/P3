namespace server.Model
{
    public class Budget
    {

        public int BudgetId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public double MonthlyAmount { get; set; }
        public double WarningAmount { get; set; }
        public double Remaining { get; set; }
        public DateTime StartDate { get; set; }
        
        
        public Budget()
        {

        }

        public Budget(int budgetId, int customerId, int accountId, double monthlyAmount, double warningAmount, DateTime startDate)
        {
            BudgetId = budgetId;
            CustomerId = customerId;
            AccountId = accountId;
            MonthlyAmount = monthlyAmount;
            WarningAmount = warningAmount;
            StartDate = startDate;
        }

        public override string ToString()
        {
            string s = $"BudgetId: {BudgetId}\nCustomerId: {CustomerId}\nAccountId: {AccountId}\nMonthlyAmount: {MonthlyAmount}\nWarningAmount: {WarningAmount}\nRemaining: {Remaining}\nStartDate: {StartDate}\n";
            return s;
        }


    }
}
