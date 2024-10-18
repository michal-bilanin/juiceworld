using System.Diagnostics;
using System.Text;
using JuiceWorld.Data;
using jwtAuth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebApi.Installers;

public static class WebApiInstaller
{
    public static IServiceCollection WebApiInstall(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddTransient<AuthService>();
        
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
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
                    new string[]{}
                }
            });
        });
        
        var secret = Environment.GetEnvironmentVariable("JWT_SECRET");
        if (secret == null)
            throw new Exception($"JWT secret is null, make sure it is specified " +
                                $"in the environment variable: JWT_SECRET");
        
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
            const string connectionStringKey = "DB_CONNECTION_STRING";
            var connectionString = Environment.GetEnvironmentVariable(connectionStringKey);

            if (connectionString == null)
            {
                Debug.Fail(
                    $"Connection string is null, make sure it is specified " +
                    $"in the environment variable: {connectionStringKey}");
                return;
            }

            options
                .UseNpgsql(connectionString)
                .LogTo(s => Debug.WriteLine(s))
                .UseLazyLoadingProxies();
        });
    
        return services;
    }
}
