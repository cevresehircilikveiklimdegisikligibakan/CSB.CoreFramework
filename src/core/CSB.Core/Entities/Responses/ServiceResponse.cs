namespace CSB.Core.Entities.Responses
{
    public sealed class ServiceResponse : ResponseBase
    {
        public static ServiceResponse Create(bool success, string message)
        {
            ServiceResponse result = new ServiceResponse();
            result.IsSuccess = success;
            result.Message = message;
            return result;
        }
        public static ServiceResponse Create(bool success)
        {
            return Create(success, string.Empty);
        }
        public static ServiceResponse Success(string message = "")
        {
            return Create(true, message);
        }
        public static ServiceResponse Fail(string message)
        {
            return Create(false, message);
        }
    }
    public class ServiceResponse<TResult> : ResponseBase where TResult : class
    {
        public ServiceResponse()
        {

        }
        public ServiceResponse(bool isSuccess, TResult data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }

        public TResult Data {  get; set; }

        public static ServiceResponse<TResult> Create(bool success, string message, TResult data)
        {
            ServiceResponse<TResult> result = new ServiceResponse<TResult>();
            result.IsSuccess = success;
            result.Message = message;                  
            result.Data = data;
            return result;
        }
        public static ServiceResponse<TResult> Create(bool success)
        {
            return Create(success, string.Empty, null);
        }
        public static ServiceResponse<TResult> Create(bool success, string message)
        {
            return Create(success, message, null);
        }
        public static ServiceResponse<TResult> Success(TResult data, string message = "")
        {
            return Create(true, message, data);
        }
        public static ServiceResponse<TResult> Fail(string message)
        {
            return Create(false, message);
        }
    }
    public abstract class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }
}