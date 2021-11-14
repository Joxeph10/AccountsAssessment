namespace Accounts.UnitTests.DomainServices
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.DataAccess;
    using Accounts.Domain.Interfaces.DomainServices;
    using Accounts.DomainServices;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

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
    }
}
