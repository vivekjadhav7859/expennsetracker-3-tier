using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models
{
    public class TransactionVm
    {
        
        public int TransactionId { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public string ExpenseType { get; set; }
        public required double Amount { get; set; }
        public string Note { get; set; }
        public DateOnly Date { get; set; }
        public string SplitWithEmail { get; set; }
        public double SplitAmount { get; set; }
    }
}
