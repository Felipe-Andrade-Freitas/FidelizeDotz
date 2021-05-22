using System.Text;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FidelizeDotz.Services.Api.Configuration
{
    public static class JwtConfig
    {
        public static void AddJwtConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var section = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(section);
            var appSettings = section.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = "Bearer";
                x.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.Valid,
                    ValidIssuer = appSettings.Issuer
                };
            });
        }
    }
}