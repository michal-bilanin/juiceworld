using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Commons.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

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
                                $"in the environment variable: JWT_SECRET");
        }

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "AuthToken"; // the cookie that stores the JWT
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
                        context.Token = context.Request.Cookies["AuthToken"];
                        return Task.CompletedTask;
                    }
                };
            });
        
        Log.Logger = new LoggerConfiguration()                       
            .WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day)       
            .WriteTo.Console()
            .MinimumLevel.Information()
            .CreateLogger();

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

        return services;
    }
}
