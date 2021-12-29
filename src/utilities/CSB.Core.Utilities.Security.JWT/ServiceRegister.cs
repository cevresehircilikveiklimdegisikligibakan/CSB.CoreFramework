using CSB.Core.Entities.Authentication;
using CSB.Core.Utilities.Security.JWT.Services;
using CSB.Core.Utilities.Security.JWT.Services.TokenServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSB.Core.Utilities.Security.JWT
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddJwtSecurityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRsaTokenService, RsaTokenService>();
            services.AddScoped<ISymmetricTokenService, SymmetricTokenService>();

            services.AddScoped<IClaimService, ClaimService>();
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}