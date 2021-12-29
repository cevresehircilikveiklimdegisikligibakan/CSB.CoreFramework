using System;

namespace CSB.Core.Entities.Authentication
{
    public record AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

}