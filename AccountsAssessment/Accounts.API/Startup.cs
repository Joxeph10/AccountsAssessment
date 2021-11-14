using Accounts.API.Interfaces;
using Accounts.API.Mappers;
using Accounts.ApplicationServices;
using Accounts.Domain.Entities;
using Accounts.Domain.Enumerations;
using Accounts.Domain.Interfaces.ApplicationServices;
using Accounts.Domain.Interfaces.DataAccess;
using Accounts.Domain.Interfaces.DomainServices;
using Accounts.DomainServices;
using Accounts.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Accounts.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configure Data Provider
            services.AddDbContext<AccountsDataContext>(options => options.UseInMemoryDatabase(databaseName: "AccountsDataBase"));

            // Dependency Injection Configuration
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerApplicationService, CustomerApplicationService>();
            services.AddTransient<ICustomerMapper, CustomerMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Mock Database Configuration
            var options = new DbContextOptionsBuilder<AccountsDataContext>()
                .UseInMemoryDatabase(databaseName: "AccountsDataBase")
                .Options;

            var context = new AccountsDataContext(options);
            DataInitialize(context);
        }

        private static void DataInitialize(AccountsDataContext context)
        {
            var customer1 = new Customer
            {
                Id = 1,
                Name = "Luke",
                Surname = "Skywalker",
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountNumber = Guid.NewGuid(),
                        Balance= 12.3,
                        CreatedByID= 1,
                        CreatedDate= DateTime.Now,
                        LastModifiedbyID=1,
                        LastModifiedDate= DateTime.Now,
                        AccountTransactions = new List<AccountTransaction>
                        {
                            new AccountTransaction
                            {
                                Amount = 12.3,
                                Comment="Account Openning",
                                CreatedByID= 1,
                                CreatedDate= DateTime.Now,
                                LastModifiedbyID= 1,
                                LastModifiedDate= DateTime.Now,
                                TransactionType = ETransactionTypes.Credit
                            }
                        }
                    },
                    new Account
                    {
                        AccountNumber = Guid.NewGuid(),
                        Balance= 0.0,
                        CreatedByID= 1,
                        CreatedDate= DateTime.Now,
                        LastModifiedbyID=1,
                        LastModifiedDate= DateTime.Now
                    }
                }
            };
            var customer2 = new Customer
            {
                Id = 2,
                Name = "Obi-Wan",
                Surname = "Kenobi"
            };
            var customer3 = new Customer { Id = 3, Name = "Han", Surname = "Solo" };
            var customer4 = new Customer { Id = 4, Name = "Boba", Surname = "Fett" };

            context.Customers.Add(customer1);
            context.Customers.Add(customer2);
            context.Customers.Add(customer3);
            context.Customers.Add(customer4);

            context.SaveChanges();
        }
    }
}
