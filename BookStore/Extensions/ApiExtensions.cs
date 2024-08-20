using BookStore.Application.Services;
using BookStore.Core.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BookStore.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration) {

            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };

                    options.Events = new JwtBearerEvents{
                        OnMessageReceived = HttpContext =>
                        {
                            HttpContext.Token = HttpContext.Request.Cookies["tasty-cookies"];

                            return Task.CompletedTask;
                        }
                };

            });

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.Requirements.Add(new PermissionRequirement(new[]
                    {
                        Permission.Create,
                        Permission.Delete,
                        Permission.Read,
                        Permission.Update
                    }));
                    
                });
                options.AddPolicy("User", policy =>
                {
                    policy.Requirements.Add(new PermissionRequirement(new[]
                    {
                        Permission.Read
                    }));

                });
            });
        }

    }
}
