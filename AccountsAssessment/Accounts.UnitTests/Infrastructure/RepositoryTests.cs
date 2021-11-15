namespace Accounts.UnitTests.Infrastructure
{
    using Accounts.Domain.Entities;
    using Accounts.Domain.Interfaces.DataAccess;
    using Accounts.Infrastructure;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class RepositoryTests
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
                .UseInMemoryDatabase(databaseName: "TestDataBase1")
                .Options;

            var context = new AccountsDataContext(options);

            IRepository _sut1 = new Repository(context);

            /// Action
            var result = _sut1.GetCustomers();

            /// Assert 
            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void WhenGetCustomersShouldReturnNonEmptyCollection()
        {
            /// Arrange
            var options = new DbContextOptionsBuilder<AccountsDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase2")
                .Options;

            var context = new AccountsDataContext(options);

            var customer3 = new Customer { Id = 3, Name = "Han", Surname = "Solo" };
            var customer4 = new Customer { Id = 4, Name = "Boba", Surname = "Fett" };

            context.Customers.Add(customer3);
            context.Customers.Add(customer4);
            context.SaveChanges();

            IRepository _sut2 = new Repository(context);

            /// Action
            var result = _sut2.GetCustomers();

            /// Assert 
            result.Should().NotBeEmpty();
            result.First().Should().BeOfType(typeof(Customer));
        }

        #endregion GetCustomers

        #region Add

        [TestMethod]
        public void WhenAddShouldReturnNonEmptyCollection()
        {
            /// Arrange
            var options = new DbContextOptionsBuilder<AccountsDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase3")
                .Options;

            var context = new AccountsDataContext(options);
            IRepository _sut3 = new Repository(context);

            var customer3 = new Customer { Id = 3, Name = "Han", Surname = "Solo" };

            /// Action
            _sut3.Add(customer3);

            /// Assert 
            _sut3.GetCustomers().Should().NotBeEmpty();
        }

        #endregion Add

        #region SaveChanges

        [TestMethod]
        public void WhenSaveChangesShouldReturnNonEmptyCollection()
        {
            /// Arrange
            var options = new DbContextOptionsBuilder<AccountsDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase4")
                .Options;

            var context = new AccountsDataContext(options);
            IRepository _sut4 = new Repository(context);

            var customer3 = new Customer { Id = 3, Name = "Han", Surname = "Solo" };
            context.Customers.Add(customer3);

            /// Action
            _sut4.SaveChanges();

            /// Assert 
            _sut4.GetCustomers().Should().NotBeEmpty();
        }

        #endregion SaveChanges
    }
}
