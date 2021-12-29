using System.Collections.Generic;

namespace CSB.Core.Entities.Authentication
{
    public record UserClaims
    {
        public User User { get; private set; }
        public IList<OperationClaim> Claims { get; private set; }

        public static UserClaims Create(User user, IList<OperationClaim> operationClaims)
        {
            return new UserClaims
            {
                User = user,
                Claims = operationClaims
            };
        }
    }
}