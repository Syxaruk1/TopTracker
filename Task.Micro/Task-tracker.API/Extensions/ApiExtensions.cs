using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Task_tracker.API.Extensions
{
    public class ApiExtensions
    {
        public void AddApiAuthentication(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CekpetKeyCekpetKeyCekpetKeyCekpetKeyCekpetKeyCekpetKey"))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["cook-ies"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
            // для контроллеров нужно будет использовать [Authorize]
        }
    }
}
