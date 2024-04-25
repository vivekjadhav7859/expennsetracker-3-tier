using Expense_Tracker.Models;

namespace expenssetracker.DAL.Interface
{
    public interface ITransaction
    {
        Task<List<Transaction>> GetTransactions(int? selectedMonth, string userId);
        Task<Transaction> GetTransactionById(int id);
        Task CreateTransaction(Transaction transaction);
        Task UpdateTransaction(Transaction transaction);
        Task DeleteTransaction(int id);
    }
}
