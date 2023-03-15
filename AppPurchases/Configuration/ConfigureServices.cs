using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Domain.ContractsServices;
using AppPurchases.Domain.Entities;
using AppPurchases.Domain.Services;
using AppPurchases.Infrastructure.AutoMapper;
using AppPurchases.Infrastructure.EventHandlers;
using AppPurchases.Infrastructure.Repositories;
using AppPurchases.Infrastructure.Validations;
using AppPurchases.Shared.Entities;
using AppPurchases.Shared.Notifications;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RabbitMQ.Client;
using Serilog;
using System.Text;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;

namespace AppPurchases.Api.Configuration
{
    public static class ConfigureServices
    {
        public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
            builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();

            return builder;
        }

        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddScoped<IPurchaseService, PurchaseService>();
            builder.Services.AddScoped<IClientService, ClientService>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<RegisterClientModel>, RegisterClientValidation>();
            builder.Services.AddScoped<IValidator<CreditCardModel>, CreditCardValidation>();
            builder.Services.AddScoped<IValidator<PurchaseModel>, PurchaseValidation>();
            builder.Services.AddScoped<IValidator<UserModel>, CredentialsValidation>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureNotifications(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IMessageBrokerService, MessageBrokerService>();
            builder.Services.AddScoped<ISubscriberPurchaseService, SubscriberPurchase>();
            builder.Services.AddScoped<IHandlePurchase, PublisherPurchase>();

            return builder;
        }

        public static WebApplicationBuilder AddCustomAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddSingleton(provider => new MapperConfiguration(m =>
                {
                    m.AddProfile(new Mappers());

                }).CreateMapper());

            return builder;
        }

        public static WebApplicationBuilder RegisterRabbitMq(this WebApplicationBuilder builder)
        {
            builder.Services
              .AddSingleton<IConnectionFactory, ConnectionFactory>(queue =>
                    new ConnectionFactory()
                    {
                        HostName = "localhost",
                        Port = 5672,
                        UserName = "root",
                        Password = "root"
                    });

            return builder;
        }

        public static WebApplicationBuilder RegisterMongoConnection(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IMongoClient, MongoClient>(
                client =>
                {
                    return new MongoClient("mongodb://localhost:27017");
                });

            return builder;
        }

        public static WebApplicationBuilder RegisterRedisCache(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddStackExchangeRedisCache(options =>
                {
                    options.InstanceName = "InstanceRedis";
                    options.Configuration = "localhost:6379";
                });

            builder.Services.AddControllers();
            return builder;
        }

        public static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e")),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return builder;
        }

        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
        {
            Host.CreateDefaultBuilder()
               .ConfigureAppConfiguration((hostingContext, config) =>
               {
                   var settings = config.Build();
                   Log.Logger = new LoggerConfiguration()
                       .Enrich.FromLogContext()
                       .WriteTo.MongoDB(settings.GetConnectionString("mongoConnection"), collectionName: "Log")
                       .CreateLogger();
               });

            return builder;
        }
    }
}
