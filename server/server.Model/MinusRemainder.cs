using System;
namespace server.Model
{
    public class MinusRemainder
    {

        public int BudgetId { get; set; }
        public int AccountId { get; set; }
        public double MonthlyAmount { get; set; }
        public double WarningAmount { get; set; }
        public double Remainder { get; set; }

        
        public MinusRemainder()
        {

        }

        public MinusRemainder(int budgetId, double remainder, int accountId, double monthlyAmount, double warningAmount)
        {
            BudgetId = budgetId;
            AccountId = accountId;
            MonthlyAmount = monthlyAmount;
            WarningAmount = warningAmount;
            Remainder = remainder;
        }


    }


}

