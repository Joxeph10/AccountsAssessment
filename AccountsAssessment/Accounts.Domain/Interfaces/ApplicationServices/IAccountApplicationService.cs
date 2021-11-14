using Accounts.Domain.Entities;

namespace Accounts.Domain.Interfaces.ApplicationServices
{
    public interface IAccountApplicationService
    {
        /// <summary>
        /// Open a new account related to a customer
        /// </summary>
        /// <param name="customerId">customer account owner</param>
        /// <param name="initialCredit">initial amount to create the account</param>
        /// <returns>New account</returns>
        public Account CreateAccount(int customerId, double initialCredit);
    }
}
