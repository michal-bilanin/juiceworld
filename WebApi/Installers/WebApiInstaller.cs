using System.Text;
using Commons.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        return services;
    }
}
