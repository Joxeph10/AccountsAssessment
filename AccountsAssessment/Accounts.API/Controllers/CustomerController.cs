namespace Accounts.API.Controllers
{
    using Accounts.API.Dto.Account;
    using Accounts.API.Dto.Customer;
    using Accounts.API.Interfaces;
    using Accounts.Domain.Interfaces.ApplicationServices;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplicationService _customerApplicationService;
        private readonly ICustomerMapper _customerMapper;
        private readonly IAccountApplicationService _accountApplicationService;
        private readonly IAccountApplicationServiceMapper _accountApplicationServiceMapper;

        public CustomerController(
            ICustomerApplicationService customerApplicationService,
            ICustomerMapper customerMapper,
            IAccountApplicationService accountApplicationService,
            IAccountApplicationServiceMapper accountApplicationServiceMapper
            )
        {
            this._customerApplicationService = customerApplicationService;
            this._customerMapper = customerMapper;
            this._accountApplicationService = accountApplicationService;
            this._accountApplicationServiceMapper = accountApplicationServiceMapper;
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

        [Route("{customerId}/account")]
        [HttpPost]
        public AccountCreationResponse CreateAccount(int customerId)
        {
            // TODO: 
            // Invoke accounts application service to create new account
            var result = this._accountApplicationService.CreateAccount(customerId);

            // Invoke mapper to generate a response DTO
            var response = this._accountApplicationServiceMapper.MapToAccountCreationResponse(result);

            return response;
        }
    }
}
