using System.Diagnostics;
using System.Text;
using Infrastructure.QueryObjects;
using Infrastructure.UnitOfWork;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using JuiceWorld.QueryObjects;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Constants;
using WebApi.Services;

namespace WebApi.Installers;

public static class WebApiInstaller
{
    public static IServiceCollection WebApiInstall(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddTransient<AuthService>();
        services.AddScoped<IUnitOfWorkProvider<UnitOfWork>, UnitOfWorkProvider>((services) =>
        {
            return new UnitOfWorkProvider(() => services.GetRequiredService<JuiceWorldDbContext>());
        });
        services.AddTransient<IQueryObject<User>, QueryObject<User>>();
        services.AddTransient<IQueryObject<Product>, QueryObject<Product>>();
        services.AddAutoMapper(typeof(WebApiInstaller));

        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "JuiceWorld WebApi", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter the token returned from the auth endpoint (only the token, without the Bearer prefix)",
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
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    []
                }
            });
        });

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
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddDbContextFactory<JuiceWorldDbContext>(options =>
        {
            var connectionString = Environment.GetEnvironmentVariable(EnvironmentConstants.DbConnectionString);

            if (connectionString == null)
            {
                throw new Exception(
                    $"Connection string is null, make sure it is specified " +
                    $"in the environment variable: {EnvironmentConstants.DbConnectionString}");
            }

            options
                .UseNpgsql(connectionString)
                .LogTo(s => Debug.WriteLine(s))
                .UseLazyLoadingProxies();
        });

        return services;
    }
}
