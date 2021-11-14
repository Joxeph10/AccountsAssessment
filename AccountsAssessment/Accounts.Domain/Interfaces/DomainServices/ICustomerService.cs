namespace Accounts.Domain.Interfaces.DomainServices
{
    using Accounts.Domain.Entities;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        /// <summary>
        /// Get the customers list
        /// </summary>
        /// <returns>list of customers</returns>
        public IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Get a customer by Id
        /// </summary>
        /// <param name="customerId">customer identification</param>
        /// <returns>Customer</returns>
        public Customer GetCustomerById(int customerId);
    }
}
