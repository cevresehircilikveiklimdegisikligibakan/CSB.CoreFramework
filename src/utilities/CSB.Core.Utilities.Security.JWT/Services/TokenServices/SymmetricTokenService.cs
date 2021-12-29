using CSB.Core.Entities.Authentication;
using CSB.Core.Entities.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace CSB.Core.Utilities.Security.JWT.Services.TokenServices
{
    internal class SymmetricTokenService : TokenServiceBase, ISymmetricTokenService
    {
        public SymmetricTokenService(IConfiguration configuration,
            ISecurityKeyService securityKeyService,
            ISigningCredentialService signingCredentialService,
            IClaimService claimService) : base(configuration, claimService, securityKeyService, signingCredentialService) { }


        public override ServiceResponse<AccessToken> CreateToken(User user, IEnumerable<OperationClaim> operationClaims)
        {
            var securityKey = _securityKeyService.CreateSymmetricSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = _signingCredentialService.CreateHmacSha256SigningCredentials(securityKey);

            var tokenResponse = CreateToken(user, operationClaims, signingCredentials);
            return tokenResponse;
        }

        public override ServiceResponse<UserClaims> GetUserClaims(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenOptions.SecurityKey));
            var userClaimsResponse = GetUserClaims(token, securityKey);
            return userClaimsResponse;
        }
    }
}