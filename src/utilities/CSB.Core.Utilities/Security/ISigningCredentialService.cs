using Microsoft.IdentityModel.Tokens;

namespace CSB.Core.Utilities.Security
{
    public interface ISigningCredentialService
    {
        SigningCredentials CreateHmacSha256SigningCredentials(SecurityKey securityKey);
        SigningCredentials CreateRsaSigningCredentials(SecurityKey securityKey);
    }
}