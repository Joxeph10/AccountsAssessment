namespace Accounts.API.Interfaces
{
    using Accounts.API.Dto.Customer;
    using Accounts.Domain.Entities;

    public interface ICustomerMapper
    {
        public CustomerResponse GetCustomerResponse(Customer customer);

    }
}
