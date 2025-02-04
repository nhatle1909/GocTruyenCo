namespace Application.Common
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = false;
        public T Result { get; set; }
        public string Message { get; set; }
        public void TryCatchResponse(Exception ex)
        {
            Success = false;
            Message = ex.Message;
        }
        public void CustomResponse(T result, bool success, string message)
        {
            Result = result;
            Success = success;
            Message = message;
        }

    }
}
