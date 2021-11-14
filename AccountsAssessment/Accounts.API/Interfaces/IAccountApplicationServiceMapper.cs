namespace Accounts.API.Interfaces
{
    using Accounts.API.Dto.Account;
    using Accounts.Domain.Entities;

    public interface IAccountApplicationServiceMapper
    {
        /// <summary>
        /// Maps the account creation response from a new account entity
        /// </summary>
        /// <param name="newAccount">new <![CDATA[Account]]> entity</param>
        /// <returns>account creation response</returns>
        public AccountCreationResponse MapToAccountCreationResponse(Account newAccount);
    }
}
