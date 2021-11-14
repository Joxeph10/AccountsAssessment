namespace Accounts.UnitTests.DomainServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Enumerations;
    using Accounts.Domain.Interfaces.DataAccess;
    using Accounts.Domain.Interfaces.DomainServices;
    using Accounts.DomainServices;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AccountTransactionServiceTests
    {
        private IAccountTransactionService _sut;
        private Mock<IRepository> _mockIRepository;

        [TestInitialize]
        public void Init()
        {
            this._mockIRepository = new Mock<IRepository>();
            this._sut = new AccountTransactionService(this._mockIRepository.Object);
        }

        #region RegisterTransaction

        [TestMethod]
        public void WhenRegisterTransactionShouldInvokeRepository()
        {
            /// Arrange
            const ETransactionTypes transactionType = ETransactionTypes.Credit;
            const double amount = 1.0;
            const string comment = "Account creation";

            this._mockIRepository
                .Setup(x => x.Add(It.IsAny<AccountTransaction>()))
                .Verifiable("Add was not Invoked");

            /// Action
            this._sut.RegisterTransaction(transactionType, amount, comment);

            /// Assert 
            this._mockIRepository.Verify(x => x.Add(It.IsAny<AccountTransaction>()), Times.Once);
        }

        [TestMethod]
        public void WhenRegisterTransactionShouldReturnAccountTransaction()
        {
            /// Arrange
            const ETransactionTypes transactionType = ETransactionTypes.Credit;
            const double amount = 1.0;
            const string comment = "Account creation";

            this._mockIRepository
                .Setup(x => x.Add(It.IsAny<AccountTransaction>()))
                .Verifiable("Add was not Invoked");

            /// Action
            var result = this._sut.RegisterTransaction(transactionType, amount, comment);

            /// Assert 
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(AccountTransaction));
        }

        #endregion RegisterTransaction
    }
}
