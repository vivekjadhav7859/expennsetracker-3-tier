using Expense_Tracker.Areas.Identity.Data;
using Expense_Tracker.Models;
using expensetracker.BLL.Interface;
using expenssetracker.DAL.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace expensetracker.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransaction _transactionRepository;
        private readonly ISummary _summaryRepository;
        private readonly UserManager<ExpenseTrackerUser> _userManager;

        public TransactionService(ITransaction transactionRepository,
                                   ISummary summaryRepository,
                                   UserManager<ExpenseTrackerUser> userManager)
        {
            _transactionRepository = transactionRepository;
            _summaryRepository = summaryRepository;
            _userManager = userManager;
        }

        public async Task<(List<Transaction> transactions, double totalIncome, double totalExpenses, double totalBalance)> GetTransactions(int? selectedMonth, string userId)
        {
            List<Transaction> transactions = await _transactionRepository.GetTransactions(selectedMonth, userId);

            if (selectedMonth.HasValue)
            {
                transactions = transactions.Where(t => t.Date.Month == selectedMonth.Value).ToList();
            }

            // Calculate total income
            var totalIncome = transactions.Where(t => t.ExpenseType == "income").Sum(t => t.Amount);

            // Calculate total expenses
            var totalExpenses = transactions.Where(t => t.ExpenseType == "expense").Sum(t => t.Amount);

            // Calculate total balance
            var totalBalance = totalIncome - totalExpenses;

            return (transactions, totalIncome, totalExpenses, totalBalance);
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _transactionRepository.GetTransactionById(id);
        }

        public async Task CreateTransaction(Transaction transaction, string splitAction, string IsRecurring, string userId, string selfUserId)
        {
            
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            if (transaction.Date < today)
            {
                await _transactionRepository.CreateTransaction(transaction);
            }
            else
            {
                bool isRecurring = IsRecurring == "true";
                if (isRecurring)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        var newTransaction = new Transaction
                        {
                            UserId = transaction.UserId,
                            CategoryId = transaction.CategoryId,
                            Category = transaction.Category,
                            ExpenseType = transaction.ExpenseType,
                            Amount = transaction.Amount,
                            Note = transaction.Note,
                            Date = transaction.Date.AddMonths(i)
                        };

                        await _transactionRepository.CreateTransaction(newTransaction);
                    }
                }
                else
                {
                    await _transactionRepository.CreateTransaction(transaction);
                }
            }

            var summary = await _summaryRepository.FindAsync(userId);
            if (summary != null)
            {
                if (transaction.ExpenseType == "expense")
                {
                    summary.TotalExpenses += transaction.Amount;
                    summary.TotalBalance = summary.TotalBalance - transaction.Amount;
                }
                else if (transaction.ExpenseType == "income")
                {
                    summary.TotalIncome += transaction.Amount;
                    summary.TotalBalance = summary.TotalBalance + transaction.Amount;
                }
                await _summaryRepository.Update(summary);
            }
            else
            {
                var newSummary = new Summary
                {
                    UserId = userId,
                    TotalIncome = transaction.ExpenseType == "income" ? transaction.Amount : 0,
                    TotalExpenses = transaction.ExpenseType == "expense" ? transaction.Amount : 0,
                    TotalBalance = transaction.ExpenseType == "income" ? transaction.Amount : -transaction.Amount
                };

                await _summaryRepository.Add(newSummary);
            }

            if (splitAction == "split" && !string.IsNullOrEmpty(transaction.SplitWithEmail))
            {
                var user = await _userManager.FindByEmailAsync(transaction.SplitWithEmail);
                if (user == null)
                {
                    return;
                }

                var splitUser2 = new Transaction
                {
                    UserId = user.Id,
                    CategoryId = transaction.CategoryId,
                    ExpenseType = transaction.ExpenseType,
                    Amount = transaction.SplitAmount,
                    Note = transaction.Note,
                    Date = transaction.Date
                };
                await _transactionRepository.CreateTransaction(splitUser2);

                var splitUser1 = new Transaction
                {
                    UserId = selfUserId,
                    CategoryId = transaction.CategoryId,
                    ExpenseType = "income",
                    Amount = transaction.SplitAmount,
                    Note = transaction.Note,
                    Date = transaction.Date
                };
                await _transactionRepository.CreateTransaction(splitUser1);
            }
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            await _transactionRepository.UpdateTransaction(transaction);
        }

        public async Task DeleteTransaction(int id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);
            var summary = await _summaryRepository.FindAsync(transaction.UserId);
            if (summary != null)
            {
                if (transaction.ExpenseType == "expense")
                {
                    summary.TotalExpenses -= transaction.Amount;
                    summary.TotalBalance = summary.TotalBalance + transaction.Amount;
                }
                else if (transaction.ExpenseType == "income")
                {
                    summary.TotalIncome -= transaction.Amount;
                    summary.TotalBalance = summary.TotalBalance - transaction.Amount;
                }

                await _summaryRepository.Update(summary);
            }
            await _transactionRepository.DeleteTransaction(id);
        }

    }
}
