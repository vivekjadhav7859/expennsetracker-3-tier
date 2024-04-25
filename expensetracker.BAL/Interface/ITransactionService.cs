using Expense_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expensetracker.BLL.Interface
{
    public interface ITransactionService
    {
        Task<(List<Transaction> transactions, double totalIncome, double totalExpenses, double totalBalance)> GetTransactions(int? selectedMonth, string userId);
        Task<Transaction> GetTransactionById(int id);
        Task CreateTransaction(Transaction transaction, string splitAction, string IsRecurring, string userId, string selfUserId);
        Task UpdateTransaction(Transaction transaction);
        Task DeleteTransaction(int id);
    }
}
