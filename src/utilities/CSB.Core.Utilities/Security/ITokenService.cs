using CSB.Core.Entities.Authentication;
using CSB.Core.Entities.Responses;
using System.Collections.Generic;

namespace CSB.Core.Utilities.Security
{
    public interface ITokenService
    {
        ServiceResponse<AccessToken> CreateToken(User user, IEnumerable<OperationClaim> operationClaims, string tokenAlgoritm);
        //ServiceResponse<string> DecodeToken(string token);
        ServiceResponse<UserClaims> GetUserClaims(string token);
    }
}