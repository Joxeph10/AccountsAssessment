namespace Accounts.ApplicationServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Enumerations;
    using Accounts.Domain.Interfaces.ApplicationServices;
    using Accounts.Domain.Interfaces.DomainServices;

    public class AccountApplicationService : IAccountApplicationService
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IAccountTransactionService _accountTransactionService;

        public AccountApplicationService(
            ICustomerService customerService,
            IAccountService accountService,
            IAccountTransactionService accountTransactionService
            )
        {
            this._customerService = customerService;
            this._accountService = accountService;
            this._accountTransactionService = accountTransactionService;
        }

        public Account CreateAccount(int customerId, double initialCredit)
        {
            Account newAccount = null;

            // invoke customer service to verify if the customer exists
            var customer = this._customerService.GetCustomerById(customerId);

            if (customer != null)
            {
                // invoke account service to create new account
                newAccount = this._accountService.CreateAccount();
                this._customerService.AddAccount(customer, newAccount);

                if (initialCredit > 0)
                {
                    var transactionType = ETransactionTypes.Credit;
                    var comment = "Account Creation";

                    // invoke transactions services to enter a new transaction
                    var transaction = this._accountTransactionService.RegisterTransaction(transactionType, initialCredit, comment);
                    this._accountService.AddTransaction(newAccount, transaction);
                }
            }

            return newAccount;
        }
    }
}
