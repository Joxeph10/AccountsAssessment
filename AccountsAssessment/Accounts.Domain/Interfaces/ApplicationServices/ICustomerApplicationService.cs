namespace Accounts.Domain.Interfaces.ApplicationServices
{
    using Accounts.Domain.Entities;
    using System.Collections.Generic;

    public interface ICustomerApplicationService
    {
        /// <summary>
        /// Get a list of customers
        /// </summary>
        /// <returns>customers list</returns>
        public IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Gets the customers information by id
        /// </summary>
        /// <param name="customerId">customer identification</param>
        /// <returns><![CDATA[Customer]]></returns>
        public Customer GetCustomerById(int customerId);
    }
}
