using Expense_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expensetracker.BLL.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(string id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task<bool> CategoryExistsAsync(string id);
    }
}
