using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Expense_Tracker.Models
{
    public class ScheduleTransactions
    {
        [Key]
        public int SchedId { get; set; }
        public int SubId { get; set; }

        public string UserId { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public string ExpenseType { get; set; }

        public required double Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }

        public DateOnly Date { get; set; }
    }
}