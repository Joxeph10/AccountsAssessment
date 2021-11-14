namespace Accounts.DomainServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Enumerations;
    using Accounts.Domain.Interfaces.DataAccess;
    using Accounts.Domain.Interfaces.DomainServices;
    using System;

    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly IRepository _repository;

        public AccountTransactionService(IRepository repository)
        {
            this._repository = repository;
        }

        public AccountTransaction RegisterTransaction(ETransactionTypes transactionType, double amouont, string comment)
        {
            var newAccountTrasaction = new AccountTransaction
            {
                //Account = newAccount,
                Amount = amouont,
                Comment = comment,
                CreatedDate = DateTime.Now,
                CreatedByID = 123,  // TODO
                LastModifiedDate = DateTime.Now,
                LastModifiedbyID = 123,  // TODO
                TransactionType = transactionType
            };

            this._repository.Add(newAccountTrasaction);

            return newAccountTrasaction;
        }
    }
}
