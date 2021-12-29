namespace CSB.Core.Web.Entities.ExceptionLogging
{
    public sealed class ExceptionLogResponse : ExceptionLogResponseBase
    {
        public static ExceptionLogResponse Create(string exceptionId)
        {
            return new ExceptionLogResponse
            {
                ExceptionId = exceptionId
            };
        }
    }
}