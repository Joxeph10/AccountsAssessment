namespace Accounts.Domain.Interfaces.DataAccess
{
    using Accounts.Domain.Entities;
    using System.Collections.Generic;

    public interface IRepository
    {
        /// <summary>
        /// Get all the customers
        /// </summary>
        /// <returns>customer list</returns>
        public IEnumerable<Customer> GetCustomers();
    }
}
