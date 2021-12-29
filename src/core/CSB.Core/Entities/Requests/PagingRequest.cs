namespace CSB.Core.Entities.Requests
{
    public abstract class PagingRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}