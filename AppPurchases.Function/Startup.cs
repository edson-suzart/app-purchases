using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Function;
using AppPurchases.Function.ContractsServices;
using AppPurchases.Function.Services;
using AppPurchases.Infrastructure.Repositories;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RabbitMQ.Client;
using System;

[assembly: WebJobsStartup(typeof(Startup))]
namespace AppPurchases.Function
{
    public sealed class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Environment.CurrentDirectory)
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables()
                   .Build();

            ConfigureServices(builder.Services, configuration);
        }

        private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IPurchaseServiceFunction, PurchaseServiceFunction>()
                .AddScoped<IPurchaseRepository, PurchaseRepository>()
                .AddSingleton<IConnectionFactory, ConnectionFactory>(queue =>
                    new ConnectionFactory()
                    {
                        HostName = "localhost",
                        Port = 5672,
                        UserName = "root",
                        Password = "root"
                    })
                .AddSingleton<IMongoClient, MongoClient>(client =>
                    new MongoClient("mongodb://localhost:27017"));
        }
    }
}