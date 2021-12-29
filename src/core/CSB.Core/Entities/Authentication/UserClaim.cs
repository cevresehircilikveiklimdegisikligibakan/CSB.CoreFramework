namespace CSB.Core.Entities.Authentication
{
    public record UserClaim
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
    }
}