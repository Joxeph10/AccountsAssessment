namespace Accounts.API.Mappers
{
    using Accounts.API.Dto.Customer;
    using Accounts.API.Interfaces;
    using Accounts.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerMapper : ICustomerMapper
    {
        public CustomerResponse GetCustomerResponse(Customer customer)
        {
            var response = new CustomerResponse
            {
                Name = customer.Name,
                Surname = customer.Surname,
                FullName = $"{customer.Name} {customer.Surname}",
                Accounts = customer.Accounts.OrderBy(o => o.CreatedDate).Select(a => GetAccountsResponse(a))
            };

            return response;
        }

        private IEnumerable<TransactionResponse> GetTransactionsResponse(IEnumerable<AccountTransaction> transactionsList)
        {
            return transactionsList
                .Select(transactionItem =>
                    new TransactionResponse
                    {
                        //Balance= 
                        Description = transactionItem.Comment,
                        Income = transactionItem.Amount,
                        TransactionDate = transactionItem.CreatedDate.ToShortDateString(),
                        TransactionType = transactionItem.TransactionType.ToString()
                    })
                .OrderByDescending(o => o.TransactionDate);
        }

        private AccountResponse GetAccountsResponse(Account account)
        {
            var transactionsList = account.AccountTransactions;

            return new AccountResponse
            {
                AccountNumber = account.AccountNumber.ToString(),
                Balance = $"{account.Balance} credits",
                Transactions = GetTransactionsResponse(transactionsList)
            };
        }
    }
}
