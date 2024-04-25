using Expense_Tracker.Areas.Identity.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Expense_Tracker.Data
{

    public class ExpenseTrackerContext : IdentityDbContext<ExpenseTrackerUser>
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<TrackTransactions> TrackTransactions { get; set; }
        public DbSet<Summary> Summaries { get; set; }

        private readonly IConfiguration _configuration;
        public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ScheduleTransactions>()
                .HasIndex(st => st.SubId);
        }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("ExpenseTrackerContextConnection");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
        public DbSet<Expense_Tracker.Models.ScheduleTransactions> ScheduleTransactions { get; set; } = default!;
    }
}