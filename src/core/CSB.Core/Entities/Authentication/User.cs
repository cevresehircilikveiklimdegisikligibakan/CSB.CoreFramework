namespace CSB.Core.Entities.Authentication
{
    public record User
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string CitizenId { get; set; }
        public string AuthenticationProviderType { get; set; }
    }
}