using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace CSB.Core.Utilities.Security.Services
{
    internal sealed class SecurityKeyService : ISecurityKeyService
    {
        public SecurityKey CreateSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public RsaSecurityKey CreateRsaSecurityKey(string securityKey, string exponent, string modulus)
        {
            var exponentByte = Base64Url.Decode(exponent);
            var modulusByte = Base64Url.Decode(modulus);

            var key = new RsaSecurityKey(new RSAParameters { Exponent = exponentByte, Modulus = modulusByte })
            {
                KeyId = securityKey
            };
            return key;
        }
    }
}