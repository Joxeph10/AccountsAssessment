namespace Accounts.API.Mappers
{
    using Accounts.API.Dto.Customer;
    using Accounts.API.Interfaces;
    using Accounts.Domain.Entities;
    using System.Linq;

    public class CustomerApplicationServiceMapper : ICustomerApplicationServiceMapper
    {
        private readonly ICustomerAccountResponseMapper _customerAccountResponseMapper;

        public CustomerApplicationServiceMapper(ICustomerAccountResponseMapper customerAccountResponseMapper)
        {
            this._customerAccountResponseMapper = customerAccountResponseMapper;
        }

        public CustomerResponse MapToCustomerResponse(Customer customer)
        {
            var response = new CustomerResponse();

            if (customer != null)
            {
                var accountResponseList = customer.Accounts
                    .OrderBy(o => o.CreatedDate)
                    .Select(account => this._customerAccountResponseMapper.MapToAccountsResponse(account));

                response.Name = customer.Name;
                response.Surname = customer.Surname;
                response.FullName = $"{customer.Name} {customer.Surname}";
                response.Accounts = accountResponseList;
            }

            return response;
        }
    }
}
