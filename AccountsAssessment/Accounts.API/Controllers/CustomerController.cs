namespace Accounts.API.Controllers
{
    using Accounts.API.Dto.Customer;
    using Accounts.API.Interfaces;
    using Accounts.Domain.Interfaces.ApplicationServices;
    using Accounts.Domain.Interfaces.DataAccess;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplicationService _customerApplicationService;
        private readonly ICustomerMapper _customerMapper;

        public CustomerController(
            ICustomerApplicationService customerApplicationService,
            ICustomerMapper customerMapper
            )
        {
            this._customerApplicationService = customerApplicationService;
            this._customerMapper = customerMapper;
        }

        [HttpGet]
        public IEnumerable<CustomerResponse> Get()
        {
            var customers = this._customerApplicationService.GetCustomers();

            return customers
                .Select(c => new CustomerResponse { Name = c.Name, Surname = c.Surname })
                .ToList();
        }

        [Route("{customerId}")]
        [HttpGet]
        public CustomerResponse Get(int customerId)
        {
            var customer = this._customerApplicationService.GetCustomerById(customerId);
            var response = this._customerMapper.GetCustomerResponse(customer);
            return response;
        }
    }
}
