using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public string ExpenseType { get; set; }
        public required double Amount { get; set; }
        public string Note { get; set; }
        [Timestamp]
        public DateOnly Date { get; set; } 
        [NotMapped]
        public string SplitWithEmail { get; set; } 
        [NotMapped]
        public double SplitAmount { get; set; }
    }
}
