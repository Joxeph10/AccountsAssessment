namespace Accounts.API.Interfaces
{
    using Accounts.API.Dto.Customer;
    using Accounts.Domain.Entities;

    public interface ICustomerApplicationServiceMapper
    {
        public CustomerResponse MapToCustomerResponse(Customer customer);

    }
}
