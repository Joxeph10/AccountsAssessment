namespace Accounts.Domain.Interfaces.DomainServices
{
    using Accounts.Domain.Entities;

    public interface IAccountService
    {
        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <returns>new account</returns>
        public Account CreateAccount();

        /// <summary>
        /// Associates a transaction to an account
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="accountTransaction">transaction</param>
        public void AddTransaction(Account account, AccountTransaction accountTransaction);
    }
}
