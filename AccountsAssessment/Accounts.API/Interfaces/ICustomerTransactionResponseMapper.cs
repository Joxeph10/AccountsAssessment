namespace Accounts.API.Interfaces
{
    using Accounts.API.Dto.Customer;
    using Accounts.Domain.Entities;

    public interface ICustomerTransactionResponseMapper
    {
        public TransactionResponse MapToTransactionsResponse(AccountTransaction accountTransaction);
    }
}
