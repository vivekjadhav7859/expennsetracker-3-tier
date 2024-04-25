using Expense_Tracker.Data;
using Expense_Tracker.Models;
using expenssetracker.DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace expenssetracker.DAL.Repositories
{
    public class TransactionRepository : ITransaction
    {
        private readonly ExpenseTrackerContext _context;

        public TransactionRepository(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactions(int? selectedMonth,string userId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return transactions;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task CreateTransaction(Transaction transaction)
        {
            // Set the Date property to the current date if it's not already set
            if (transaction.Date == default(DateOnly))
            {
                transaction.Date = DateOnly.FromDateTime(DateTime.Today);
            }


            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            _context.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
