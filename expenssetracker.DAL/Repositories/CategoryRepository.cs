using Expense_Tracker.Data;
using Expense_Tracker.Models;
using expenssetracker.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.DAL
{
    public class CategoryRepository : ICategory
    {
        private readonly ExpenseTrackerContext _context;

        public CategoryRepository(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsAsync(string id)
        {
            return await _context.Categories.AnyAsync(e => e.CategoryId == id);
        }
    }
}
