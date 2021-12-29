using CSB.Core.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace CSB.Core.Utilities.Security.JWT.Services
{
    internal sealed class ClaimService : IClaimService
    {
        public Claim GetEmailClaim(string email)
        {
            return new Claim(JwtRegisteredClaimNames.Email, email);
        }
        public string GetEmailClaim(IEnumerable<Claim> claims)
        {
            return claims.Where(x => x.Type == JwtRegisteredClaimNames.Email).FirstOrDefault().Value;
        }

        public Claim GetNameClaim(string name)
        {
            return new Claim(ClaimTypes.Name, name);
        }
        public string GetNameClaim(IEnumerable<Claim> claims)
        {
            return claims.Where(x => x.Type == ClaimTypes.Name)?.FirstOrDefault()?.Value;
        }

        public Claim GetNameIdentifierClaim(string nameIdentifier)
        {
            return new Claim(ClaimTypes.NameIdentifier, nameIdentifier);
        }
        public string GetNameIdentifierClaim(IEnumerable<Claim> claims)
        {
            return claims.Where(x => x.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;
        }

        public Claim GetNameUniqueIdentifierClaim(string nameUniqueIdentifier)
        {
            return new Claim(ClaimTypes.SerialNumber, nameUniqueIdentifier);
        }
        public string GetNameUniqueIdentifierClaim(IEnumerable<Claim> claims)
        {
            return claims.Where(x => x.Type == ClaimTypes.SerialNumber)?.FirstOrDefault()?.Value;
        }

        public IEnumerable<Claim> GetRoleClaim(string[] roles)
        {
            return roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
        }
        public string[] GetRoleClaim(IEnumerable<Claim> claims)
        {
            return claims.Where(x => x.Type == ClaimTypes.Role)?.Select(x => x.Value)?.ToArray();
        }

        public IEnumerable<Claim> SetClaims(User user, IEnumerable<OperationClaim> operationClaims)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(GetNameIdentifierClaim(user.Id.ToString()));
            if (!string.IsNullOrWhiteSpace(user.CitizenId))
                claims.Add(GetNameUniqueIdentifierClaim(user.CitizenId));
            if (!string.IsNullOrWhiteSpace(user.DisplayName))
                claims.Add(GetNameClaim(user.DisplayName));
            claims.AddRange(GetRoleClaim(operationClaims.Select(c => c.Name).ToArray()));
            claims.Add(new Claim("AuthenticationProviderType", user.AuthenticationProviderType));

            return claims;
        }
        public UserClaims GetClaims(IEnumerable<Claim> claims)
        {
            User user = new User();
            user.Id = Convert.ToInt32(GetNameIdentifierClaim(claims));
            user.CitizenId = GetNameUniqueIdentifierClaim(claims);
            user.DisplayName = GetNameClaim(claims);
            user.AuthenticationProviderType = claims.Where(x => x.Type == "AuthenticationProviderType")?.FirstOrDefault()?.Value;

            string[] roles = GetRoleClaim(claims);
            IList<OperationClaim> operationClaims = roles.Select(x => new OperationClaim
            {
                Name = x
            }).ToList();

            return UserClaims.Create(user, operationClaims);
        }
    }
}