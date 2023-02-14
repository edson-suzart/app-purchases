using AppPurchases.Api.Configuration;
using AppPurchases.Shared.Exceptions;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder);
await using var app = builder.Build();
ConfigureApplication(app);
app.Run();

static void RegisterServices(WebApplicationBuilder builder)
{
    builder.RegisterMongoConnection();
    builder.RegisterRabbitMq();
    builder.AddServices();
    builder.AddRepositories();
    builder.ConfigureValidators();
    builder.ConfigureNotifications();
    builder.AddCustomAutoMapper();
    builder.RegisterRedisCache();
    builder.ConfigureAuthentication();
    builder.AddSerilog();

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<UnhandledExceptionFilter>();
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme.",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                          Array.Empty<string>()
                    }
                });
        options.EnableAnnotations();
    });
}

static void ConfigureApplication(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppPurchases.API v1"));
    }

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppPurchases.API v1"));
    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
