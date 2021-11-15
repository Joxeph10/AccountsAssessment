namespace Accounts.UnitTests.Mappers
{
    using Accounts.API.Dto.Customer;
    using Accounts.API.Interfaces;
    using Accounts.API.Mappers;
    using Accounts.Domain.Entities;
    using Accounts.Domain.Enumerations;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class CustomerTransactionResponseMapperTests
    {
        private ICustomerTransactionResponseMapper _sut;

        [TestInitialize]
        public void Init()
        {
            this._sut = new CustomerTransactionResponseMapper();
        }

        #region MapToTransactionsResponse

        [TestMethod]
        public void WhenMapToTransactionsResponseShouldReturnTransactionsResponse()
        {
            /// Arrange
            var accountTransaction = new AccountTransaction 
            {
                Comment = "Comment",
                Amount = 12.3,
                CreatedDate= DateTime.Now,
                TransactionType = ETransactionTypes.Credit
            };

            var expectedResult = new TransactionResponse
            {
                Description = accountTransaction.Comment,
                Income = accountTransaction.Amount,
                TransactionDate = accountTransaction.CreatedDate.ToShortDateString(),
                TransactionType = accountTransaction.TransactionType.ToString()
            };

            /// Action
            var result = this._sut.MapToTransactionsResponse(accountTransaction);

            /// Assert 
            result.Should().BeEquivalentTo(expectedResult);
        }


        #endregion MapToTransactionsResponse
    }
}
