namespace Accounts.API.Mappers
{
    using Accounts.API.Dto.Account;
    using Accounts.API.Interfaces;
    using Accounts.Domain.Entities;

    public class AccountApplicationServiceMapper : IAccountApplicationServiceMapper
    {
        public AccountCreationResponse MapToAccountCreationResponse(Account newAccount)
        {
            var response = new AccountCreationResponse();

            if (newAccount != null)
            {
                response.AccountNumber = newAccount.AccountNumber.ToString();
                response.Balance = $"{newAccount.Balance} Credits";
                response.Message = $"The account {newAccount.AccountNumber} was successfuly created.";
            }

            return response;
        }
    }
}
