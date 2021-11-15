namespace Accounts.UnitTests.Mappers
{
    using Accounts.API.Dto.Customer;
    using Accounts.API.Interfaces;
    using Accounts.API.Mappers;
    using Accounts.Domain.Entities;
    using Accounts.Domain.Enumerations;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class CustomerApplicationServiceMapperTests
    {
        private ICustomerApplicationServiceMapper _sut;
        private Mock<ICustomerAccountResponseMapper> _MockCustomerAccountResponseMapper;

        [TestInitialize]
        public void Init()
        {
            this._MockCustomerAccountResponseMapper = new Mock<ICustomerAccountResponseMapper>();

            this._sut = new CustomerApplicationServiceMapper(this._MockCustomerAccountResponseMapper.Object);
        }

        #region MapToCustomerResponse

        [TestMethod]
        public void WhenMapToCustomerResponseShouldSkipMapToAccountsResponse()
        {
            /// Arrange
            Customer customer1 = null;

            /// Action
            this._sut.MapToCustomerResponse(customer1);

            /// Assert 
            this._MockCustomerAccountResponseMapper.Verify(x => x.MapToAccountsResponse(It.IsAny<Account>()), Times.Never);
        }

        [TestMethod]
        public void WhenMapToCustomerResponseShouldEmptyCustomerResponse()
        {
            /// Arrange
            Customer customer1 = null;

            var expectedResult = new CustomerResponse();

            /// Action
            var result = this._sut.MapToCustomerResponse(customer1);

            /// Assert 
            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public void WhenMapToCustomerResponseShouldReturnCustomerResponse()
        {
            /// Arrange
            var accountTransaction = new AccountTransaction
            {
                Comment = "Comment",
                Amount = 12.3,
                CreatedDate = DateTime.Now,
                TransactionType = ETransactionTypes.Credit
            };

            var account = new Account
            {
                Id = 1,
                AccountNumber = Guid.NewGuid(),
                AccountTransactions = new List<AccountTransaction> { accountTransaction }
            };

            var customer = new Customer
            {
                Id = 1,
                Name = "customer.Name",
                Surname = "customer.Surname",
                Accounts = new List<Account> { account }
            };

            var expectedTransactionResponse = new TransactionResponse
            {
                Description = accountTransaction.Comment,
                Income = accountTransaction.Amount,
                TransactionDate = accountTransaction.CreatedDate.ToShortDateString(),
                TransactionType = accountTransaction.TransactionType.ToString()
            };

            var expectedAccountResponse = new AccountResponse
            {
                AccountNumber = account.AccountNumber.ToString(),
                Balance = $"{account.Balance} credits",
                Transactions = new List<TransactionResponse> { expectedTransactionResponse }
            };

            var expectedResult = new CustomerResponse
            {
                Name = customer.Name,
                Surname = customer.Surname,
                FullName = $"{customer.Name} {customer.Surname}",
                Accounts = new List<AccountResponse> { expectedAccountResponse }
            };

            this._MockCustomerAccountResponseMapper
                .Setup(x => x.MapToAccountsResponse(It.IsAny<Account>()))
                .Returns(expectedAccountResponse)
                .Verifiable("MapToAccountsResponse was not invoked.");

            /// Action
            var result = this._sut.MapToCustomerResponse(customer);

            /// Assert 
            result.Should().BeOfType(typeof(CustomerResponse));
            result.Name.Should().Be(expectedResult.Name);
            result.Surname.Should().Be(expectedResult.Surname);
            result.FullName.Should().Be(expectedResult.FullName);

            result.Accounts.First().AccountNumber.Should().Be(expectedAccountResponse.AccountNumber);
            result.Accounts.First().Balance.Should().Be(expectedAccountResponse.Balance);

            result.Accounts.First().Transactions.First().Description.Should().Be(expectedTransactionResponse.Description);
            result.Accounts.First().Transactions.First().Income.Should().Be(expectedTransactionResponse.Income);
            result.Accounts.First().Transactions.First().TransactionDate.Should().Be(expectedTransactionResponse.TransactionDate);
            result.Accounts.First().Transactions.First().TransactionType.Should().Be(expectedTransactionResponse.TransactionType);
        }

        #endregion MapToCustomerResponse
    }
}
