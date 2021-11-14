namespace Accounts.API.Controllers
{
    using Accounts.API.Dto;
    using Accounts.Domain.Interfaces.DataAccess;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository _repository;

        public CustomerController(IRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public IEnumerable<CustomerResponse> Get()
        {
            var customers = this._repository.GetCustomers();

            return customers
                .Select(c => new CustomerResponse { Name = c.Name, Surname = c.Surname })
                .ToList();
        }
    }
}
