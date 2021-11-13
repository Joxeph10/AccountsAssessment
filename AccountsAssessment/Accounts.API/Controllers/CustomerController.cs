namespace Accounts.API.Controllers
{
    using Accounts.API.Dto;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<CustomerResponse> Get()
        {
            return new List<CustomerResponse>
            {
                new CustomerResponse { Name = "Luke", Surname = "Skywalker" },
                new CustomerResponse { Name = "Han", Surname = "Solo" },
                new CustomerResponse { Name = "Obi-Wan", Surname = "Kenobi" },
                new CustomerResponse { Name = "Boba", Surname = "Fett" },
                new CustomerResponse { Name = "Jango", Surname = "Fett" }
            };
        }
    }
}
