namespace Accounts.API.Dto.Customer
{
    using System.Collections.Generic;

    public class CustomerResponse
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public IEnumerable<AccountResponse> Accounts { get; set; }
    }
}
