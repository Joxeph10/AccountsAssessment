namespace Accounts.API.Mappers
{
    using Accounts.API.Dto.Customer;
    using Accounts.API.Interfaces;
    using Accounts.Domain.Entities;
    using System.Linq;

    public class CustomerAccountResponseMapper : ICustomerAccountResponseMapper
    {
        private readonly ICustomerTransactionResponseMapper _customerTransactionResponseMapper;

        public CustomerAccountResponseMapper(ICustomerTransactionResponseMapper customerTransactionResponseMapper)
        {
            this._customerTransactionResponseMapper = customerTransactionResponseMapper;
        }

        public AccountResponse MapToAccountsResponse(Account account)
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
