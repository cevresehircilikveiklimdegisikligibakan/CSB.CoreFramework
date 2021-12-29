using CSB.Core.Entities.Authentication;
using CSB.Core.Entities.Responses;
using CSB.Core.Services;
using CSB.Core.Utilities.Security.JWT.Entities;
using CSB.Core.Utilities.Security.JWT.Services.TokenServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace CSB.Core.Utilities.Security.JWT.Services
{

    internal class TokenService : ITokenService
    {
        private readonly ISerializer _serializer;
        private readonly IRsaTokenService _rsaTokenService;
        private readonly ISymmetricTokenService _symmetricTokenService;

        public TokenService(IRsaTokenService rsaTokenService,
                            ISymmetricTokenService symmetricTokenService,
                            ISerializer serializer)
        {
            _rsaTokenService = rsaTokenService;
            _symmetricTokenService = symmetricTokenService;
            _serializer = serializer;
        }

        public ServiceResponse<AccessToken> CreateToken(User user, IEnumerable<OperationClaim> operationClaims, string tokenAlgoritm)
        {
            var tokenService = GetTokenServiceByAlgoritm(tokenAlgoritm);
            var response = tokenService.CreateToken(user, operationClaims);
            return response;
        }

        public ServiceResponse<UserClaims> GetUserClaims(string token)
        {
            var tokenService = GetTokenService(token);
            var response = tokenService.GetUserClaims(token);
            return response;
        }

        private ITokenAlgoritmService GetTokenService(string token)
        {
            string tokenAlgoritm = GetTokenAlgoritm(token);
            var tokenService = GetTokenServiceByAlgoritm(tokenAlgoritm);
            return tokenService;
        }
        private ITokenAlgoritmService GetTokenServiceByAlgoritm(string tokenAlgoritm)
        {
            ITokenAlgoritmService tokenService = null;
            if (tokenAlgoritm == SecurityAlgoritmConstants.RS256)
            {
                tokenService = _rsaTokenService;
            }
            else if (tokenAlgoritm == SecurityAlgoritmConstants.Symmetric || tokenAlgoritm == SecurityAlgoritmConstants.SHA256)
            {
                tokenService = _symmetricTokenService;
            }
            return tokenService;
        }
        private string GetTokenAlgoritm(string token)
        {
            var decodedTokenResponse = DecodeToken(token);
            string decodedToken = decodedTokenResponse.Data;
            if (decodedTokenResponse.Data.IndexOf("}.{") >= 0)
            {
                decodedToken = decodedTokenResponse.Data.Substring(0, decodedTokenResponse.Data.IndexOf("}.{") + 1);
            }
            JwtToken decodedJwtToken = _serializer.Deserialize<JwtToken>(decodedToken);
            return decodedJwtToken?.Algoritm;
        }
        private ServiceResponse<string> DecodeToken(string token)
        {
            if (token.StartsWith($"{JwtBearerDefaults.AuthenticationScheme} "))
                token = token.Substring($"{JwtBearerDefaults.AuthenticationScheme} ".Length);

            string decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token).ToString();
            if (string.IsNullOrEmpty(decodedToken))
                return ServiceResponse<string>.Fail("");
            return ServiceResponse<string>.Success(decodedToken);
        }
    }
}