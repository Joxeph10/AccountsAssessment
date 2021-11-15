namespace Accounts.API.Mappers
{
    using Accounts.API.Dto.Customer;
    using Accounts.API.Interfaces;
    using Accounts.Domain.Entities;

    public class CustomerTransactionResponseMapper : ICustomerTransactionResponseMapper
    {
        public TransactionResponse MapToTransactionsResponse(AccountTransaction accountTransaction)
        {
            return new TransactionResponse
            {
                Description = accountTransaction.Comment,
                Income = accountTransaction.Amount,
                TransactionDate = accountTransaction.CreatedDate.ToShortDateString(),
                TransactionType = accountTransaction.TransactionType.ToString()
            };
        }
    }
}
