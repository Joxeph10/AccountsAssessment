namespace Accounts.UnitTests.ApplicationServices
{
    using Accounts.ApplicationServices;
    using Accounts.Domain.Entities;
    using Accounts.Domain.Enumerations;
    using Accounts.Domain.Interfaces.ApplicationServices;
    using Accounts.Domain.Interfaces.DomainServices;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AccountApplicationServiceTests
    {
        private IAccountApplicationService _sut;

        private Mock<ICustomerService> _mockCustomerService;
        private Mock<IAccountService> _mockAccountService;
        private Mock<IAccountTransactionService> _mockAccountTransactionService;

        [TestInitialize]
        public void Init()
        {
            this._mockAccountService = new Mock<IAccountService>();
            this._mockAccountTransactionService = new Mock<IAccountTransactionService>();
            this._mockCustomerService = new Mock<ICustomerService>();

            this._sut = new AccountApplicationService(
                this._mockCustomerService.Object,
                this._mockAccountService.Object,
                this._mockAccountTransactionService.Object);
        }

        #region CreateAccount

        [TestMethod]
        public void WhenCreateAccountShouldInvokeCustomerService()
        {
            /// Arrange
            const int CustomerId = 1;
            const double InitialCredit = 0.0;

            this._mockCustomerService
                .Setup(x => x.GetCustomerById(CustomerId))
                .Verifiable("GetCustomerById was not Invoked");

            /// Action
            this._sut.CreateAccount(CustomerId, InitialCredit);

            /// Assert 
            this._mockCustomerService.Verify(x => x.GetCustomerById(CustomerId), Times.Once);
        }

        [TestMethod]
        public void WhenCreateAccountShouldSkipAccountService()
        {
            /// Arrange
            const int CustomerId = 1;
            const double InitialCredit = 0.0;

            this._mockCustomerService
                .Setup(x => x.GetCustomerById(CustomerId))
                .Verifiable("GetCustomerById was not Invoked");

            /// Action
            this._sut.CreateAccount(CustomerId, InitialCredit);

            /// Assert 
            this._mockAccountService.Verify(x => x.CreateAccount(), Times.Never);
        }

        [TestMethod]
        public void WhenCreateAccountWithNonExistantCustomerShouldReturnNullAccount()
        {
            /// Arrange
            const int CustomerId = 1;
            const double InitialCredit = 0.0;

            this._mockCustomerService
                .Setup(x => x.GetCustomerById(CustomerId))
                .Verifiable("GetCustomerById was not Invoked");

            /// Action
            var result = this._sut.CreateAccount(CustomerId, InitialCredit);

            /// Assert 
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenCreateAccountShouldInvokeCreateAccount()
        {
            /// Arrange
            const int CustomerId = 1;
            const double InitialCredit = 0.0;

            var customer = new Customer { Id = CustomerId };

            var account = new Account();

            this._mockCustomerService
                .Setup(x => x.GetCustomerById(CustomerId))
                .Returns(customer);

            this._mockAccountService
                .Setup(x => x.CreateAccount())
                .Returns(account)
                .Verifiable("CreateAccount was not Invoked");

            this._mockCustomerService
                .Setup(x => x.AddAccount(customer, account));

            /// Action
            this._sut.CreateAccount(CustomerId, InitialCredit);

            /// Assert 
            this._mockAccountService.Verify(x => x.CreateAccount(), Times.Once);
        }

        [TestMethod]
        public void WhenCreateAccountShouldInvokeAddAccount()
        {
            /// Arrange
            const int CustomerId = 1;
            const double InitialCredit = 0.0;

            var customer = new Customer { Id = CustomerId };

            var account = new Account();

            this._mockCustomerService
                .Setup(x => x.GetCustomerById(CustomerId))
                .Returns(customer);

            this._mockAccountService
                .Setup(x => x.CreateAccount())
                .Returns(account);

            this._mockCustomerService
                .Setup(x => x.AddAccount(customer, account))
                .Verifiable("CreateAccount was not Invoked");

            /// Action
            this._sut.CreateAccount(CustomerId, InitialCredit);

            /// Assert 
            this._mockCustomerService.Verify(x => x.AddAccount(customer, account), Times.Once);
        }

        [TestMethod]
        public void WhenCreateAccountShouldSkipRegisterTransaction()
        {
            /// Arrange
            const int CustomerId = 1;
            const double InitialCredit = 0.0;

            var customer = new Customer { Id = CustomerId };

            var account = new Account();

            this._mockCustomerService
                .Setup(x => x.GetCustomerById(CustomerId))
                .Returns(customer);

            this._mockAccountService
                .Setup(x => x.CreateAccount())
                .Returns(account);

            this._mockCustomerService
                .Setup(x => x.AddAccount(customer, account))
                .Verifiable("CreateAccount was not Invoked");

            /// Action
            this._sut.CreateAccount(CustomerId, InitialCredit);

            /// Assert 
            this._mockAccountTransactionService
                .Verify(x => x.RegisterTransaction(
                    It.IsAny<ETransactionTypes>(),
                    It.Is<double>(y => y == 0.0),
                    It.IsAny<string>())
                , Times.Never);
        }

        [TestMethod]
        public void WhenCreateAccountShouldInvokeRegisterTransaction()
        {
            /// Arrange
            const int CustomerId = 1;
            const double InitialCredit = 12.0;

            var customer = new Customer { Id = CustomerId };

            var account = new Account();

            var accountTransaction = new AccountTransaction();

            this._mockCustomerService
                .Setup(x => x.GetCustomerById(CustomerId))
                .Returns(customer);

            this._mockAccountService
                .Setup(x => x.CreateAccount())
                .Returns(account);

            this._mockCustomerService
                .Setup(x => x.AddAccount(customer, account));

            this._mockAccountTransactionService
                .Setup(x => x.RegisterTransaction(
                    It.IsAny<ETransactionTypes>(),
                    It.Is<double>(y => y > 0.0),
                    It.IsAny<string>()))
                .Returns(accountTransaction)
                .Verifiable("RegisterTransaction was not Invoked");

            this._mockAccountService
                .Setup(x => x.AddTransaction(account, accountTransaction));

            /// Action
            this._sut.CreateAccount(CustomerId, InitialCredit);

            /// Assert 
            this._mockAccountTransactionService
                .Verify(x => x.RegisterTransaction(
                    It.IsAny<ETransactionTypes>(),
                    It.Is<double>(y => y > 0.0),
                    It.IsAny<string>())
                , Times.Once);
        }

        [TestMethod]
        public void WhenCreateAccountShouldInvokeAddTransaction()
        {
            /// Arrange
            const int CustomerId = 1;
            const double InitialCredit = 12.0;

            var customer = new Customer { Id = CustomerId };

            var account = new Account();

            var accountTransaction = new AccountTransaction();

            this._mockCustomerService
                .Setup(x => x.GetCustomerById(CustomerId))
                .Returns(customer);

            this._mockAccountService
                .Setup(x => x.CreateAccount())
                .Returns(account);

            this._mockCustomerService
                .Setup(x => x.AddAccount(customer, account));

            this._mockAccountTransactionService
                .Setup(x => x.RegisterTransaction(
                    It.IsAny<ETransactionTypes>(),
                    It.Is<double>(y => y > 0.0),
                    It.IsAny<string>()))
                .Returns(accountTransaction);

            this._mockAccountService
                .Setup(x => x.AddTransaction(account, accountTransaction))
                .Verifiable("AddTransaction was not Invoked");

            /// Action
            this._sut.CreateAccount(CustomerId, InitialCredit);

            /// Assert 
            this._mockAccountService.Verify(x => x.AddTransaction(account, accountTransaction), Times.Once);
        }

        [TestMethod]
        public void WhenCreateAccountShouldReturnAccount()
        {
            /// Arrange
            const int CustomerId = 1;
            const double InitialCredit = 12.0;

            var customer = new Customer { Id = CustomerId };

            var account = new Account { Id = 123 };

            var accountTransaction = new AccountTransaction();

            this._mockCustomerService
                .Setup(x => x.GetCustomerById(CustomerId))
                .Returns(customer);

            this._mockAccountService
                .Setup(x => x.CreateAccount())
                .Returns(account);

            this._mockCustomerService
                .Setup(x => x.AddAccount(customer, account));

            this._mockAccountTransactionService
                .Setup(x => x.RegisterTransaction(
                    It.IsAny<ETransactionTypes>(),
                    It.Is<double>(y => y > 0.0),
                    It.IsAny<string>()))
                .Returns(accountTransaction);

            this._mockAccountService
                .Setup(x => x.AddTransaction(account, accountTransaction))
                .Verifiable("AddTransaction was not Invoked");

            /// Action
            var result = this._sut.CreateAccount(CustomerId, InitialCredit);

            /// Assert 
            result.Should().BeOfType(typeof(Account));
            result.Id.Should().Be(account.Id);
        }

        #endregion CreateAccount
    }
}
