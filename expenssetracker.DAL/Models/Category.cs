using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Category
    {
        [Key]
        [StringLength(50)]
        public string CategoryId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Icon { get; set; } = "";
    }
}
