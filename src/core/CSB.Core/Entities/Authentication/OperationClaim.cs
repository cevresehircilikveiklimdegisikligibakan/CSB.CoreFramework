namespace CSB.Core.Entities.Authentication
{
    public record OperationClaim
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
    }
}