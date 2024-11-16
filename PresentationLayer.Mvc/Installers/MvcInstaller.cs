using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Commons.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using AutoMapper;
using MongoDB.Driver;
using Serilog.Events;

namespace PresentationLayer.Mvc.Installers;

public static class MvcInstaller
{
    public static IServiceCollection MvcInstall(this IServiceCollection services)
    {
        services.AddControllers();

        var secret = Environment.GetEnvironmentVariable(EnvironmentConstants.JwtSecret);
        if (secret == null)
        {
            throw new Exception($"JWT secret is null, make sure it is specified " +
                                $"in the environment variable: {EnvironmentConstants.JwtSecret}");
        }

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = Constants.JWT_TOKEN; // the cookie that stores the JWT
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[Constants.JWT_TOKEN];
                        return Task.CompletedTask;
                    }
                };
            });

        // Configure Logging
        var connectionString = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbConnectionString);
        var collectionName = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbCollectionName);
        var databaseName = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbDatabaseName);

        if (connectionString is null || collectionName is null || databaseName is null)
        {
            Debug.Fail(
                $"Logging database connection string, collection name or database name is null, make sure they are specified " +
                $"in the environment variables: " +
                $"{EnvironmentConstants.LoggingDbConnectionString} ({connectionString}), " +
                $"{EnvironmentConstants.LoggingDbCollectionName} ({collectionName}), " +
                $"{EnvironmentConstants.LoggingDbDatabaseName} ({databaseName})");
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
        
        return services;
    }
}
