namespace Accounts.Domain.Interfaces.DomainServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Enumerations;

    public interface IAccountTransactionService
    {
        /// <summary>
        /// Register a new account transaction
        /// </summary>
        /// <param name="transactionType">Could be a Debit or a Credit operation</param>
        /// <param name="amouont">value of the transaction</param>
        /// <param name="comment">Message to give more detail to the transaction</param>
        /// <returns>new Account Transaction</returns>
        public AccountTransaction RegisterTransaction(ETransactionTypes transactionType, double amouont, string comment);
    }
}
