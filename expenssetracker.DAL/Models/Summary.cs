using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models
{
    public class Summary
    {
        [Key]
        public string UserId { get; set; }
        public double TotalIncome { get; set; }
        public double TotalExpenses { get; set; }
        public double TotalBalance { get; set; }
        public Summary()
        {
            TotalIncome = 0;
            TotalExpenses = 0;
            CalculateBalance();
        }

        public void CalculateBalance()
        {
            TotalBalance = TotalIncome - TotalExpenses;
        }
    }
}
