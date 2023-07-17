using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UATP.RapidPay.Interfaces.Settings;
using UATP.RapidPay.Shared.Jwt;

namespace UATP.RapidPay.API
{
    public static class Configure
    {
        public static void AddJwtManager(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration.GetSection("Jwt").Get<JwtConfiguration>();

            var jwtManager = new JwtManager(configuration);

            builder.Services.AddSingleton<IJwtManager>(jwtManager);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = configuration.Issuer,
                      ValidAudience = configuration.Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecretKey))
                  };
              });
        }

        public static void InitConfig(this WebApplicationBuilder builder)
        {
            ApplicationSettings.Init(new ApplicationSettings
            {
                EncryptKey = builder.Configuration["EncryptKey"]
            });
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme ('Bearer' and then your JWT)",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });
        }
    }
}
