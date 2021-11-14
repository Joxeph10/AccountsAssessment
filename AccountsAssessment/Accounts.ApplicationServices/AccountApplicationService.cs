namespace Accounts.ApplicationServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.ApplicationServices;
    using System;

    public class AccountApplicationService : IAccountApplicationService
    {
        public Account CreateAccount(int customerId)
        {
            // TODO:
            // invoke customer service to verify if the customer exists
            // invoke account service to create new account
            // invoke transactions services to enter a new transaction

            throw new NotImplementedException();
        }
    }
}
