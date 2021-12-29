using Microsoft.IdentityModel.Tokens;

namespace CSB.Core.Utilities.Security
{
    public interface ISecurityKeyService
    {
        SecurityKey CreateSymmetricSecurityKey(string key);
        RsaSecurityKey CreateRsaSecurityKey(string securityKey, string exponent, string modulus);
    }
}