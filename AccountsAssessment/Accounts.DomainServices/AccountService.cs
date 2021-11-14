namespace Accounts.DomainServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.DataAccess;
    using Accounts.Domain.Interfaces.DomainServices;
    using System;

    public class AccountService : IAccountService
    {
        private readonly IRepository _repository;

        public AccountService(IRepository repository)
        {
            this._repository = repository;
        }

        public Account CreateAccount()
        {
            var newAccount = new Account
            {
                AccountNumber = Guid.NewGuid(),
                Balance = 0.0,
                CreatedByID = 1,    // TODO: replace with user
                CreatedDate = DateTime.Now,
                LastModifiedbyID = 1,    // TODO: replace with user
                LastModifiedDate = DateTime.Now
            };

            this._repository.Add<Account>(newAccount);
            return newAccount;
        }
    }
}
