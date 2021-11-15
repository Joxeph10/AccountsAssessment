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
    public class CustomerAccountResponseMapperTests
    {
        private ICustomerAccountResponseMapper _sut;
        private Mock<ICustomerTransactionResponseMapper> _mockCustomerTransactionResponseMapper;

        [TestInitialize]
        public void Init()
        {
            this._mockCustomerTransactionResponseMapper = new Mock<ICustomerTransactionResponseMapper>();
            this._sut = new CustomerAccountResponseMapper(this._mockCustomerTransactionResponseMapper.Object);
        }

        #region MapToAccountsResponse

        [TestMethod]
        public void WhenMapToAccountsResponseShouldInvokeMapToTransactionsResponse()
        {
            /// Arrange
            var accountTransaction = new AccountTransaction
            {
                Id = 1,
                Comment = "Comment",
                Amount = 12.3,
                CreatedDate = DateTime.Now,
                TransactionType = ETransactionTypes.Credit
            };

            var account = new Account
            {
                AccountNumber = Guid.NewGuid(),
                Balance = 12.3,
                AccountTransactions = new List<AccountTransaction>
                {
                    accountTransaction
                }
            };

            var expectedTransactionResponse = new TransactionResponse
            {
                Description = accountTransaction.Comment,
                Income = accountTransaction.Amount,
                TransactionDate = accountTransaction.CreatedDate.ToShortDateString(),
                TransactionType = accountTransaction.TransactionType.ToString()
            };

            this._mockCustomerTransactionResponseMapper
                .Setup(x => x.MapToTransactionsResponse(It.Is<AccountTransaction>(y => y.Id == accountTransaction.Id)))
                .Returns(expectedTransactionResponse)
                .Verifiable("MapToTransactionsResponse was not invoked.");

            /// Action
            this._sut.MapToAccountsResponse(account);

            /// Assert 
            this._mockCustomerTransactionResponseMapper
                .Verify(x => x.MapToTransactionsResponse(It.Is<AccountTransaction>(y => y.Id == accountTransaction.Id)), Times.Once);
        }

        [TestMethod]
        public void WhenMapToAccountsResponseShouldReturnAccountResponse()
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
                AccountNumber = Guid.NewGuid(),
                Balance = 12.3,
                AccountTransactions = new List<AccountTransaction>
                {
                    accountTransaction
                }
            };

            var expectedTransactionResponse = new TransactionResponse
            {
                Description = accountTransaction.Comment,
                Income = accountTransaction.Amount,
                TransactionDate = accountTransaction.CreatedDate.ToShortDateString(),
                TransactionType = accountTransaction.TransactionType.ToString()
            };

            var expectedResult = new AccountResponse
            {
                AccountNumber = account.AccountNumber.ToString(),
                Balance = $"{account.Balance} credits",
                Transactions = new List<TransactionResponse>
                {
                    expectedTransactionResponse
                }
            };

            this._mockCustomerTransactionResponseMapper
                .Setup(x => x.MapToTransactionsResponse(accountTransaction))
                .Returns(expectedTransactionResponse)
                .Verifiable("MapToTransactionsResponse was not invoked.");

            /// Action
            var result = this._sut.MapToAccountsResponse(account);

            /// Assert 
            result.AccountNumber.Should().Be(expectedResult.AccountNumber);
            result.Balance.Should().Be(expectedResult.Balance);
            result.Transactions.First().Description.Should().Be(expectedTransactionResponse.Description);
            result.Transactions.First().Income.Should().Be(expectedTransactionResponse.Income);
            result.Transactions.First().TransactionDate.Should().Be(expectedTransactionResponse.TransactionDate);
            result.Transactions.First().TransactionType.Should().Be(expectedTransactionResponse.TransactionType);
        }

        #endregion MapToAccountsResponse
    }
}
