namespace Accounts.ApplicationServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.ApplicationServices;
    using Accounts.Domain.Interfaces.DomainServices;
    using System.Collections.Generic;

    public class CustomerApplicationService : ICustomerApplicationService
    {
        private readonly ICustomerService _customerService;

        public CustomerApplicationService(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return this._customerService.GetCustomers();
        }

        public Customer GetCustomerById(int customerId)
        {
            return this._customerService.GetCustomerById(customerId);
        }
    }
}
