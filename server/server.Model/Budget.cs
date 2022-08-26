namespace server.Model
{
    public class Budget
    {

        public int BudgetId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public double MonthlyAmount { get; set; }
        public double WarningAmount { get; set; }
        public DateTime Date { get; set; }

        public Budget()
        {

        }

        public Budget(int budgetId, int customerId, int accountId, double monthlyAmount, double warningAmount, DateTime date)
        {
            BudgetId = budgetId;
            CustomerId = customerId;
            AccountId = accountId;
            MonthlyAmount = monthlyAmount;
            WarningAmount = warningAmount;
            Date = date;
        }

    }
}
