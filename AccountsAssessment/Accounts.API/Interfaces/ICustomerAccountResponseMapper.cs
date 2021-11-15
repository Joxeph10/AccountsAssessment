namespace Accounts.API.Interfaces
{
    using Accounts.API.Dto.Customer;
    using Accounts.Domain.Entities;

    public interface ICustomerAccountResponseMapper
    {
        public AccountResponse MapToAccountsResponse(Account account);
    }
}
