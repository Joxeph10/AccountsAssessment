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
    using System.Linq;

    [TestClass]
    class CustomerServiceTests
    {
        private ICustomerService _sut;
        private Mock<IRepository> _mockIRepository;

        [TestInitialize]
        public void Init()
        {
            this._mockIRepository = new Mock<IRepository>();
            this._sut = new CustomerService(this._mockIRepository.Object);
        }

        #region GetCustomers

        [TestMethod]
        public void WhenGetCustomersShouldInvokeRepository()
        {
            /// Arrange
            this._mockIRepository
                .Setup(x => x.GetCustomers())
                .Verifiable("GetCustomers was not Invoked");

            /// Action
            this._sut.GetCustomers();

            /// Assert 
            this._mockIRepository.Verify(x => x.GetCustomers(), Times.Once);
        }

        [TestMethod]
        public void WhenGetCustomerByIdShouldReturnEmptyList()
        {
            /// Arrange

            this._mockIRepository
                .Setup(x => x.GetCustomers())
                .Verifiable("GetCustomers was not Invoked");

            /// Action
            var result = this._sut.GetCustomers();

            /// Assert 
            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void WhenGetCustomerByIdShouldReturnListofCustomer()
        {
            /// Arrange
            var customerList = new List<Customer>
            {
                new Customer { Id = 1, Name = "name1", Surname = "surname1" },
                new Customer { Id = 2, Name = "name2", Surname = "surname2" },
                new Customer { Id = 3, Name = "name3", Surname = "surname3" }
            };

            this._mockIRepository
                .Setup(x => x.GetCustomers())
                .Returns(customerList)
                .Verifiable("GetCustomers was not Invoked");

            /// Action
            var result = this._sut.GetCustomers();

            /// Assert 
            result.Should().NotBeNullOrEmpty();
            result.First().Should().BeOfType(typeof(Customer));
        }

        #endregion GetCustomerById

        #region GetCustomerById

        [TestMethod]
        public void WhenGetCustomerByIdShouldInvokeRepository()
        {
            /// Arrange
            const int customerId = 1;

            this._mockIRepository
                .Setup(x => x.GetCustomers())
                .Verifiable("GetCustomers was not Invoked");

            /// Action
            this._sut.GetCustomerById(customerId);

            /// Assert 
            this._mockIRepository.Verify(x => x.GetCustomers(), Times.Once);
        }

        [TestMethod]
        public void WhenGetCustomerByIdShouldReturnNullCustomer()
        {
            /// Arrange
            const int customerId = 1;

            this._mockIRepository
                .Setup(x => x.GetCustomers())
                .Verifiable("GetCustomers was not Invoked");

            /// Action
            var result = this._sut.GetCustomerById(customerId);

            /// Assert 
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenGetCustomerByIdShouldReturnACustomer()
        {
            /// Arrange
            const int customerId = 1;

            var customerList = new List<Customer>
            {
                new Customer { Id = 1, Name = "name1", Surname = "surname1" },
                new Customer { Id = 2, Name = "name2", Surname = "surname2" },
                new Customer { Id = 3, Name = "name3", Surname = "surname3" }
            };

            this._mockIRepository
                .Setup(x => x.GetCustomers())
                .Returns(customerList)
                .Verifiable("GetCustomers was not Invoked");

            /// Action
            var result = this._sut.GetCustomerById(customerId);

            /// Assert 
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Customer));
            result.Id.Should().Be(customerId);
        }

        #endregion GetCustomerById
    }
}
