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
    using System.Collections.Generic;

    [TestClass]
    public class CustomerMapperTests
    {
        private ICustomerMapper _sut;

        [TestInitialize]
        public void Init()
        {
            this._sut = new CustomerMapper();
        }

        #region GetCustomerResponse

        [TestMethod]
        public void WhenGetCustomerResponseShouldReturnCustomerResponse()
        {
            /// Arrange
            var transaction1 = new AccountTransaction
            {
                Amount = 12.3,
                Comment = "Account Openning",
                CreatedByID = 1,
                CreatedDate = DateTime.Now,
                LastModifiedbyID = 1,
                LastModifiedDate = DateTime.Now,
                TransactionType = ETransactionTypes.Credit
            };

            var account1 = new Account
            {
                AccountNumber = Guid.NewGuid(),
                Balance = 12.3,
                CreatedByID = 1,
                CreatedDate = DateTime.Now,
                LastModifiedbyID = 1,
                LastModifiedDate = DateTime.Now,
                AccountTransactions = new List<AccountTransaction>
                {
                    transaction1
                }
            };

            var account2 = new Account
            {
                AccountNumber = Guid.NewGuid(),
                Balance = 0.0,
                CreatedByID = 1,
                CreatedDate = DateTime.Now,
                LastModifiedbyID = 1,
                LastModifiedDate = DateTime.Now
            };

            var customer1 = new Customer
            {
                Id = 1,
                Name = "Luke",
                Surname = "Skywalker",
                Accounts = new List<Account>
                {
                    account1,
                    account2
                }
            };

            var expectedResult = new CustomerResponse
            {
                Name = "Luke",
                Surname = "Skywalker",
                FullName = "Luke Skywalker",
                Accounts = new List<AccountResponse>
                {
                    new AccountResponse
                    {
                        AccountNumber = account1.AccountNumber.ToString(),
                        Balance= account1.Balance.ToString(),
                        Transactions = new List<TransactionResponse>
                        {
                            new TransactionResponse
                            {
                                TransactionDate = transaction1.CreatedDate.ToShortDateString(),
                                Description= transaction1.Comment,
                                Income=transaction1.Amount,
                                TransactionType= transaction1.TransactionType.ToString()
                            }
                        }
                    },
                    new AccountResponse
                    {
                        AccountNumber = account2.AccountNumber.ToString(),
                        Balance= account2.Balance.ToString()
                    },
                }
            };

            /// Action
            var result = this._sut.GetCustomerResponse(customer1);

            /// Assert 
            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public void WhenGetCustomerResponseShouldReturnEmptyCustomerResponse()
        {
            /// Arrange
            Customer customer1 = null;
            var expectedResult = new CustomerResponse();

            /// Action
            var result = this._sut.GetCustomerResponse(customer1);

            /// Assert 
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetCustomerResponse

    }
}
