using Expense_Tracker.Models;
using expenssetracker.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expenssetracker.DAL.Repositories
{
    public class SummaryRepository : ISummary
    {
        public Task Add(Summary summary)
        {
            throw new NotImplementedException();
        }

        public Task<Summary> FindAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task Update(Summary summary)
        {
            throw new NotImplementedException();
        }
    }
}
