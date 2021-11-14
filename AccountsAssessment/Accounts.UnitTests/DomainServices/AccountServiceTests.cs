namespace Accounts.UnitTests.DomainServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.DataAccess;
    using Accounts.Domain.Interfaces.DomainServices;
    using Accounts.DomainServices;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;

    [TestClass]
    public class AccountServiceTests
    {
        private IAccountService _sut;
        private Mock<IRepository> _mockIRepository;

        [TestInitialize]
        public void Init()
        {
            this._mockIRepository = new Mock<IRepository>();
            this._sut = new AccountService(this._mockIRepository.Object);
        }

        #region CreateAccount

        [TestMethod]
        public void WhenCreateAccountShouldInvokeRepository()
        {
            /// Arrange
            this._mockIRepository
                .Setup(x => x.Add(It.IsAny<Account>()))
                .Verifiable("Add was not Invoked");

            /// Action
            this._sut.CreateAccount();

            /// Assert 
            this._mockIRepository.Verify(x => x.Add(It.IsAny<Account>()), Times.Once);
        }

        [TestMethod]
        public void WhenCreateAccountShouldReturnAccount()
        {
            /// Arrange
            this._mockIRepository
                .Setup(x => x.Add(It.IsAny<Account>()))
                .Verifiable("Add was not Invoked");

            /// Action
            var result = this._sut.CreateAccount();

            /// Assert 
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Account));
        }

        #endregion CreateAccount

        #region AddTransaction

        [TestMethod]
        public void WhenAddTransactionWithNullTransactionsShouldInvokeRepository()
        {
            /// Arrange
            var account = new Account();
            var accountTransaction = new AccountTransaction();

            this._mockIRepository
                .Setup(x => x.SaveChanges())
                .Verifiable("SaveChanges was not Invoked");

            /// Action
            this._sut.AddTransaction(account, accountTransaction);

            /// Assert 
            this._mockIRepository.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void WhenAddTransactionShouldInvokeRepository()
        {
            /// Arrange
            var account = new Account { AccountTransactions = new List<AccountTransaction>() };
            var accountTransaction = new AccountTransaction();

            this._mockIRepository
                .Setup(x => x.SaveChanges())
                .Verifiable("SaveChanges was not Invoked");

            /// Action
            this._sut.AddTransaction(account, accountTransaction);

            /// Assert 
            this._mockIRepository.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void WhenAddTransactionShouldCalculateBalance()
        {
            /// Arrange
            const double ExpectedResult = 10.0;

            var account = new Account
            {
                AccountTransactions = new List<AccountTransaction>
                {
                    new AccountTransaction { Id = 1, Amount = 1 },
                    new AccountTransaction { Id = 2, Amount = 2 },
                    new AccountTransaction { Id = 3, Amount = 3 },
                    new AccountTransaction { Id = 4, Amount = 4 },
                    new AccountTransaction { Id = 5, Amount = 5 }
                }
            };
            var accountTransaction = new AccountTransaction
            {
                Id = 1,
                Amount = -5.0,
                TransactionType = Domain.Enumerations.ETransactionTypes.Debit
            };

            this._mockIRepository
                .Setup(x => x.SaveChanges())
                .Verifiable("SaveChanges was not Invoked");

            /// Action
            this._sut.AddTransaction(account, accountTransaction);

            /// Assert 
            account.Balance.Should().Be(ExpectedResult);
        }

        #endregion AddTransaction
    }
}
