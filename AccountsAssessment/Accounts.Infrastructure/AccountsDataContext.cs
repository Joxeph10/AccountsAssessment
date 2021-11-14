namespace Accounts.Infrastructure
{
    using Accounts.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AccountsDataContext : DbContext
    {
        public AccountsDataContext(DbContextOptions<AccountsDataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
    }
}
