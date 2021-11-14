using Accounts.API.Dto.Account;
using Accounts.API.Interfaces;
using Accounts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.API.Mappers
{
    public class AccountApplicationServiceMapper : IAccountApplicationServiceMapper
    {
        public AccountCreationResponse MapToAccountCreationResponse(Account newAccount)
        {
            throw new NotImplementedException();
        }
    }
}
