using Expense_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expenssetracker.DAL.Interface
{
    public interface ISummary
    {
        Task<Summary> FindAsync(string userId);
        Task Add(Summary summary);
        Task Update(Summary summary);
    }
}
