namespace CSB.Core.Entities.Authentication
{
    public record TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
        public string Exponent { get; set; }
        public string Modulus { get; set; }
    }
}