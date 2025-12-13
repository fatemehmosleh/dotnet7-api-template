using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Common.AspNetCore.Security
{
    public static class ServiceCollectionExtension
    {
        public static void AddCustomAuthentication(this IServiceCollection services, JsonWebKey jsonWebKey)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Clock skew compensates for server time drift.
                        // We recommend 5 minutes or less:
                        ClockSkew = TimeSpan.FromMinutes(5),
                        // Specify the key used to sign the token:
                        IssuerSigningKey = jsonWebKey,
                        RequireSignedTokens = true,
                        // Ensure the token hasn't expired:
                        RequireExpirationTime = false,
                        ValidateLifetime = false,
                        // Ensure the token audience matches our audience value (default true):
                        ValidateAudience = false,
                        ValidAudience = "",
                        // Ensure the token was issued by a trusted authorization server (default true):
                        ValidateIssuer = false,
                        ValidIssuer = ""
                    };
                    options.Events = new JwtBearerEvents
                    {
                        // ...
                        OnMessageReceived = context =>
                        {
                            string authorization = context.Request.Headers["X-JWT-Assertion"];

                            // If no authorization header found, nothing to process further
                            if (string.IsNullOrEmpty(authorization))
                            {
                                context.NoResult();
                                return Task.CompletedTask;
                            }


                            context.Token = authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                                ? authorization["Bearer ".Length..].Trim()
                                : authorization.Trim();


                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}