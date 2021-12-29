namespace CSB.Core.Entities.Authentication
{
    public record GroupClaim
    {
        public int GroupId { get; set; }
        public int ClaimId { get; set; }
    }
}