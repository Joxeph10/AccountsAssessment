namespace Accounts.API.Dto.Customer
{
    using System.Collections.Generic;

    public class AccountResponse
    {
        public string AccountNumber { get; set; }
        public string Balance { get; set; }
        public IEnumerable<TransactionResponse> Transactions { get; set; }
    }
}
