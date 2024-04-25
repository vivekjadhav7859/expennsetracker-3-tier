using Expense_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expenssetracker.DAL.Interface
{
    public interface ICategory
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(string id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task<bool> CategoryExistsAsync(string id);
    }
}
