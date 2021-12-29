using Microsoft.IdentityModel.Tokens;

namespace CSB.Core.Utilities.Security.Services
{
    internal sealed class SigningCredentialService : ISigningCredentialService
    {
        public SigningCredentials CreateHmacSha256SigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
        public SigningCredentials CreateRsaSigningCredentials(SecurityKey securityKey)
        {
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);
            return signingCredentials;
        }
    }
}