namespace CSB.Core.Web.Entities.ExceptionLogging
{
    public sealed class ValidationLogResponse : ExceptionLogResponseBase
    {
        public string ValidationMessage { get; set; }

        public static ValidationLogResponse Create(string exceptionId, string validationMessage)
        {
            return new ValidationLogResponse
            {
                ExceptionId = exceptionId,
                ValidationMessage = validationMessage
            };
        }
    }
}