using CSB.Core.Entities.Authentication;
using CSB.Core.Entities.Responses;
using IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CSB.Core.Utilities.Security.JWT.Services.TokenServices
{
    internal class RsaTokenService : TokenServiceBase, IRsaTokenService
    {
        public RsaTokenService(IConfiguration configuration,
            ISecurityKeyService securityKeyService,
            ISigningCredentialService signingCredentialService,
            IClaimService claimService) : base(configuration, claimService, securityKeyService, signingCredentialService) { }


        public override ServiceResponse<AccessToken> CreateToken(User user, IEnumerable<OperationClaim> operationClaims)
        {
            var securityKey = _securityKeyService.CreateRsaSecurityKey(_tokenOptions.SecurityKey, _tokenOptions.Exponent, _tokenOptions.Modulus);
            var signingCredentials = _signingCredentialService.CreateRsaSigningCredentials(securityKey);

            var tokenResponse = CreateToken(user, operationClaims, signingCredentials);
            return tokenResponse;
        }

        public override ServiceResponse<UserClaims> GetUserClaims(string token)
        {
            var exponent = Base64Url.Decode(_tokenOptions.Exponent);
            var modulus = Base64Url.Decode(_tokenOptions.Modulus);
            var securityKey = new RsaSecurityKey(new RSAParameters { Exponent = exponent, Modulus = modulus })
            {
                KeyId = _tokenOptions.SecurityKey
            }; var userClaimsResponse = GetUserClaims(token, securityKey);
            return userClaimsResponse;
        }
    }
}