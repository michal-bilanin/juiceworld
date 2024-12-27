using System.Diagnostics;
using System.Text;
using Commons.Constants;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Serilog;
using Serilog.Events;

namespace WebApi.Installers;

public static class WebApiInstaller
{
    public static IServiceCollection WebApiInstall(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Configure Logging
        var connectionString = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbConnectionString);
        var collectionName = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbCollectionName);
        var databaseName = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbDatabaseName);

        if (connectionString is null || collectionName is null || databaseName is null)
        {
            Log.Warning(
                $"Logging database connection string, collection name or database name is null, make sure they are specified " +
                $"in the environment variables: " +
                $"{EnvironmentConstants.LoggingDbConnectionString} ({connectionString}), " +
                $"{EnvironmentConstants.LoggingDbCollectionName} ({collectionName}), " +
                $"{EnvironmentConstants.LoggingDbDatabaseName} ({databaseName}). Using the default logger.");
        }
        else
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MongoDBCapped(database, collectionName: collectionName)
                .WriteTo.Console(LogEventLevel.Information)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog();
            });
        }

        // Configure Swagger
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "JuiceWorld WebApi", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description =
                    "Please enter the token returned from the auth endpoint (only the token, without the Bearer prefix)",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    []
                }
            });
        });

        services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<JuiceWorldDbContext>()
            .AddDefaultTokenProviders();

        // Configure JWT
        var secret = Environment.GetEnvironmentVariable(EnvironmentConstants.JwtSecret);
        if (secret == null)
        {
            throw new Exception($"JWT secret is null, make sure it is specified " +
                                $"in the environment variable: JWT_SECRET");
        }

        return services;
    }
}
