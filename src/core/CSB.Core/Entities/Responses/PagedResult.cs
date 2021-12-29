using System.Collections.Generic;

namespace CSB.Core.Entities.Responses
{
    public class PagedResult<TResult> : PagedResultBase where TResult : class, new()
    {
        public IList<TResult> Results { get; set; }

        public PagedResult()
        {
            Results = new List<TResult>();
        }
    }
}