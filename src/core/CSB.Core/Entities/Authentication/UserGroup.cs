namespace CSB.Core.Entities.Authentication
{
    public record UserGroup
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}