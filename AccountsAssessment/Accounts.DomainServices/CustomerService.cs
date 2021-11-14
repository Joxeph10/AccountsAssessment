﻿namespace Accounts.DomainServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.DataAccess;
    using Accounts.Domain.Interfaces.DomainServices;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private readonly IRepository _repository;

        public CustomerService(IRepository repository)
        {
            this._repository = repository;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return this._repository.GetCustomers();
        }

        public Customer GetCustomerById(int customerId)
        {
            return this._repository.GetCustomers().FirstOrDefault(c => c.Id == customerId);
        }
    }
}
