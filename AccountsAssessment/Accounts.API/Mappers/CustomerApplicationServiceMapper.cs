﻿namespace Accounts.API.Mappers
{
    using Accounts.API.Dto.Customer;
    using Accounts.API.Interfaces;
    using Accounts.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerApplicationServiceMapper : ICustomerApplicationServiceMapper
    {
        private readonly ICustomerTransactionResponseMapper _customerTransactionResponseMapper;

        public CustomerApplicationServiceMapper(ICustomerTransactionResponseMapper customerTransactionResponseMapper)
        {
            this._customerTransactionResponseMapper = customerTransactionResponseMapper;
        }

        public CustomerResponse GetCustomerResponse(Customer customer)
        {
            var response = new CustomerResponse();

            if (customer != null)
            {
                response.Name = customer.Name;
                response.Surname = customer.Surname;
                response.FullName = $"{customer.Name} {customer.Surname}";
                response.Accounts = customer.Accounts.OrderBy(o => o.CreatedDate).Select(a => GetAccountsResponse(a));
            }

            return response;
        }

        private AccountResponse GetAccountsResponse(Account account)
        {
            var transactionsResponsesList = account.AccountTransactions
                .Select(transaction => this._customerTransactionResponseMapper.MapToTransactionsResponse(transaction));

            return new AccountResponse
            {
                AccountNumber = account.AccountNumber.ToString(),
                Balance = $"{account.Balance} credits",
                Transactions = transactionsResponsesList
            };
        }
    }
}
