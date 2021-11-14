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
    }
}
