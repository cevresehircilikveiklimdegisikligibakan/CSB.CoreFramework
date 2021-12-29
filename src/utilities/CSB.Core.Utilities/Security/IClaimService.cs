using CSB.Core.Entities.Authentication;
using System.Collections.Generic;
using System.Security.Claims;

namespace CSB.Core.Utilities.Security
{
    public interface IClaimService
    {
        Claim GetEmailClaim(string email);
        string GetEmailClaim(IEnumerable<Claim> claims);

        Claim GetNameClaim(string name);
        string GetNameClaim(IEnumerable<Claim> claims);

        Claim GetNameIdentifierClaim(string nameIdentifier);
        string GetNameIdentifierClaim(IEnumerable<Claim> claims);

        Claim GetNameUniqueIdentifierClaim(string nameUniqueIdentifier);
        string GetNameUniqueIdentifierClaim(IEnumerable<Claim> claims);

        IEnumerable<Claim> GetRoleClaim(string[] roles);
        string[] GetRoleClaim(IEnumerable<Claim> claims);

        IEnumerable<Claim> SetClaims(User user, IEnumerable<OperationClaim> operationClaims);
        UserClaims GetClaims(IEnumerable<Claim> claims);
    }
}