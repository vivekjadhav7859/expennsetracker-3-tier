using Expense_Tracker.Models;
using expensetracker.BLL.Interface;
using expenssetracker.DAL.Interface;
namespace expensetracker.BLL.Services
{
    public class CategoryService : ICategoryService
    {   
        private readonly ICategory _categoryRepository;

        public CategoryService(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task<bool> CategoryExistsAsync(string id)
        {
            return await _categoryRepository.CategoryExistsAsync(id);
        }
    }
}
