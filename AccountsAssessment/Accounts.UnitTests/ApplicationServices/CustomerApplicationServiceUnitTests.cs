namespace Accounts.UnitTests.ApplicationServices
{
    using Accounts.ApplicationServices;
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.ApplicationServices;
    using Accounts.Domain.Interfaces.DomainServices;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class CustomerApplicationServiceUnitTests
    {
        private ICustomerApplicationService _sut;
        private Mock<ICustomerService> _mockICustomerService;

        [TestInitialize]
        public void Init()
        {
            this._mockICustomerService = new Mock<ICustomerService>();
            this._sut = new CustomerApplicationService(this._mockICustomerService.Object);
        }

        #region GetCustomers

        [TestMethod]
        public void WhenGetCustomersShouldInvokeCustomerService()
        {
            /// Arrange
            this._mockICustomerService
                .Setup(x => x.GetCustomers())
                .Verifiable("GetCustomers was not Invoked");

            /// Action
            this._sut.GetCustomers();

            /// Assert 
            this._mockICustomerService.Verify(x => x.GetCustomers(), Times.Once);
        }

        [TestMethod]
        public void WhenGetCustomersShouldReturnEmptyList()
        {
            /// Arrange
            this._mockICustomerService
                .Setup(x => x.GetCustomers())
                .Verifiable("GetCustomers was not Invoked");

            /// Action
            var result = this._sut.GetCustomers();

            /// Assert 
            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void WhenGetCustomerShouldReturnANotEmptyList()
        {
            /// Arrange

            var customer = new Customer { Id = 1, Name = "name1", Surname = "surname1" };

            var customerList = new List<Customer> { customer };

            this._mockICustomerService
               .Setup(x => x.GetCustomers())
               .Returns(customerList)
               .Verifiable("GetCustomers was not Invoked");

            /// Action
            var result = this._sut.GetCustomers();

            /// Assert 
            result.Should().NotBeNullOrEmpty();
            result.First().Should().BeOfType(typeof(Customer));
        }

        #endregion GetCustomers

        #region GetCustomerById

        [TestMethod]
        public void WhenGetCustomerByIdShouldInvokeCustomerService()
        {
            /// Arrange
            const int customerId = 1;

            this._mockICustomerService
                .Setup(x => x.GetCustomerById(customerId))
                .Verifiable("GetCustomerById was not Invoked");

            /// Action
            this._sut.GetCustomerById(customerId);

            /// Assert 
            this._mockICustomerService.Verify(x => x.GetCustomerById(customerId), Times.Once);
        }

        [TestMethod]
        public void WhenGetCustomerByIdShouldReturnNullCustomer()
        {
            /// Arrange
            const int customerId = 1;

            this._mockICustomerService
                .Setup(x => x.GetCustomerById(customerId))
                .Verifiable("GetCustomerById was not Invoked");

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

            var customer = new Customer { Id = 1, Name = "name1", Surname = "surname1" };

            this._mockICustomerService
               .Setup(x => x.GetCustomerById(customerId))
               .Returns(customer)
               .Verifiable("GetCustomerById was not Invoked");

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
