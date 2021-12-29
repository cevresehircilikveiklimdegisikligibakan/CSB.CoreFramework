using CSB.Core.Entities.Authentication;
using CSB.Core.Entities.Responses;
using System.Collections.Generic;

namespace CSB.Core.Utilities.Security.JWT.Services.TokenServices
{
    internal interface ITokenAlgoritmService
    {
        ServiceResponse<AccessToken> CreateToken(User user, IEnumerable<OperationClaim> operationClaims);
        ServiceResponse<UserClaims> GetUserClaims(string token);
    }
}