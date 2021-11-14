namespace Accounts.UnitTests.Infrastructure
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.DataAccess;
    using Accounts.Infrastructure;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Linq;

    [TestClass]
    class RepositoryTests
    {
        [TestInitialize]
        public void Init()
        {
        }

        #region GetCustomers

        [TestMethod]
        public void WhenGetCustomersShouldReturnEmptyCollection()
        {
            /// Arrange
            var options = new DbContextOptionsBuilder<AccountsDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase")
                .Options;

            var context = new AccountsDataContext(options);

            IRepository _sut = new Repository(context);

            /// Action
            var result = _sut.GetCustomers();

            /// Assert 
            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void WhenGetCustomersShouldReturnNonEmptyCollection()
        {
            /// Arrange
            var options = new DbContextOptionsBuilder<AccountsDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase")
                .Options;

            var context = new AccountsDataContext(options);

            var customer3 = new Customer { Id = 3, Name = "Han", Surname = "Solo" };
            var customer4 = new Customer { Id = 4, Name = "Boba", Surname = "Fett" };

            context.Customers.Add(customer3);
            context.Customers.Add(customer4);
            context.SaveChanges();

            IRepository _sut = new Repository(context);

            /// Action
            var result = _sut.GetCustomers();

            /// Assert 
            result.Should().NotBeEmpty();
            result.First().Should().BeOfType(typeof(Customer));
        }

        #endregion GetCustomers
    }
}
