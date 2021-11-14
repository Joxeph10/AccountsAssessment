namespace Accounts.Infrastructure
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class Repository : IRepository
    {
        private readonly AccountsDataContext _dataContext;

        public Repository(AccountsDataContext accountsDataContext)
        {
            this._dataContext = accountsDataContext;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return this._dataContext.Customers
                .Include(c => c.Accounts)
                .ThenInclude(a => a.AccountTransactions);
        }
    }
}
