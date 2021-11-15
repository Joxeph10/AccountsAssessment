namespace Accounts.Domain.Interfaces.DataAccess
{
    using Accounts.Domain.Entities;
    using System.Collections.Generic;

    public interface IRepository
    {
        /// <summary>
        /// Get all the customers
        /// </summary>
        /// <returns>customer list</returns>
        public IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Adds an entity to the data context
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entity">new entity</param>
        public void Add<TEntity>(TEntity entity);

        /// <summary>
        /// Save changes on the data context
        /// </summary>
        public void SaveChanges();
    }
}
