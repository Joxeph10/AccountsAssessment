namespace Accounts.UnitTests.Mappers
{
    using Accounts.API.Dto.Account;
    using Accounts.API.Interfaces;
    using Accounts.API.Mappers;
    using Accounts.Domain.Entities;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class AccountApplicationServiceMapperTests
    {
        private IAccountApplicationServiceMapper _sut;

        [TestInitialize]
        public void Init()
        {
            this._sut = new AccountApplicationServiceMapper();
        }

        #region MapToAccountCreationResponse

        [TestMethod]
        public void WhenMapToAccountCreationResponseGetCustomerResponseShouldReturnEmptyAccountCreationResponse()
        {
            /// Arrange
            Account account1 = null;

            var expectedResult = new AccountCreationResponse();

            /// Action
            var result = this._sut.MapToAccountCreationResponse(account1);

            /// Assert 
            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public void WhenMapToAccountCreationResponseGetCustomerResponseShouldReturnAccountCreationResponse()
        {
            /// Arrange
            var account1 = new Account
            {
                AccountNumber = Guid.NewGuid(),
                Balance = 12.3
            };

            var expectedResult = new AccountCreationResponse
            {
                AccountNumber = account1.AccountNumber.ToString(),
                Balance = $"{account1.Balance} Credits",
                Message = $"The account {account1.AccountNumber} was successfuly created."
            };

            /// Action
            var result = this._sut.MapToAccountCreationResponse(account1);

            /// Assert 
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion MapToAccountCreationResponse
    }
}
