namespace Accounts.ApplicationServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.ApplicationServices;
    using Accounts.Domain.Interfaces.DomainServices;

    public class AccountApplicationService : IAccountApplicationService
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public AccountApplicationService(
            ICustomerService customerService,
            IAccountService accountService
            )
        {
            this._customerService = customerService;
            this._accountService = accountService;
        }

        public Account CreateAccount(int customerId)
        {
            Account newAccount = null;

            // invoke customer service to verify if the customer exists
            var customer = this._customerService.GetCustomerById(customerId);

            if (customer != null)
            {
                // invoke account service to create new account
                newAccount = this._accountService.CreateAccount();

                this._customerService.AddAccount(customer, newAccount);

                // invoke transactions services to enter a new transaction
            }

            return newAccount;
        }
    }
}
