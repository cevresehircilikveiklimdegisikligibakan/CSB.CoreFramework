using CSB.Core.Entities.Authentication;
using CSB.Core.Entities.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CSB.Core.Utilities.Security.JWT.Services.TokenServices
{
    internal abstract class TokenServiceBase : ITokenAlgoritmService
    {
        #region [ Variables ]
        private readonly IClaimService _claimService;
        private protected readonly ISecurityKeyService _securityKeyService;
        private protected readonly ISigningCredentialService _signingCredentialService;

        private protected readonly TokenOptions _tokenOptions;

        private protected DateTime _tokenExpiration;
        #endregion

        #region [ Constructor]
        public TokenServiceBase(IConfiguration configuration,
                                IClaimService claimService,
                                ISecurityKeyService securityKeyService,
                                ISigningCredentialService signingCredentialService)
        {
            _claimService = claimService;
            _securityKeyService = securityKeyService;
            _signingCredentialService = signingCredentialService;

            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        #endregion

        #region [ Interface Implementations ]
        public abstract ServiceResponse<AccessToken> CreateToken(User user, IEnumerable<OperationClaim> operationClaims);

        public abstract ServiceResponse<UserClaims> GetUserClaims(string token);
        #endregion

        #region [ Protected Methods ]
        private protected ServiceResponse<AccessToken> CreateToken(User user, IEnumerable<OperationClaim> operationClaims, SigningCredentials signingCredentials)
        {
            _tokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityTokenResponse = CreateJwtSecurityToken(user, signingCredentials, operationClaims);
            if (securityTokenResponse.IsSuccess == false)
                return ServiceResponse<AccessToken>.Fail(securityTokenResponse.Message);
            var tokenResponse = WriteToken(securityTokenResponse.Data);
            if (tokenResponse.IsSuccess == false)
                return ServiceResponse<AccessToken>.Fail(tokenResponse.Message);
            var accessToken = new AccessToken
            {
                Token = tokenResponse.Data,
                Expiration = _tokenExpiration
            };
            return ServiceResponse<AccessToken>.Success(accessToken);
        }

        private protected ServiceResponse<JwtSecurityToken> CreateJwtSecurityToken(User user, SigningCredentials signingCredentials, IEnumerable<OperationClaim> operationClaims)
        {
            var token = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: _tokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
                );
            return ServiceResponse<JwtSecurityToken>.Success(token);
        }

        private protected IEnumerable<Claim> SetClaims(User user, IEnumerable<OperationClaim> operationClaims)
        {
            return _claimService.SetClaims(user, operationClaims);
        }

        private protected ServiceResponse<string> WriteToken(JwtSecurityToken securityToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(securityToken);
            return ServiceResponse<string>.Success(token);
        }

        private protected ServiceResponse<UserClaims> GetUserClaims(string token, SecurityKey securityKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out validatedToken);
            }
            catch (SecurityTokenValidationException)
            {
                return ServiceResponse<UserClaims>.Fail("Token süresi doldu");
            }
            catch (Exception)
            {
                return ServiceResponse<UserClaims>.Fail("");
            }
            var jwtToken = (JwtSecurityToken)validatedToken;
            UserClaims userClaims = _claimService.GetClaims(jwtToken.Claims);
            return ServiceResponse<UserClaims>.Success(userClaims);
        }
        #endregion
    }
}