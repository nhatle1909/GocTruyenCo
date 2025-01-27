namespace Application.Common
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = false;
        public T Result { get; set; }
        public string Message { get; set; } = "Failed Response";
        public void SuccessCreateResponse()
        {
            Success = true;
            Message = "Create successful";
        }
        public void SuccessRetrieveResponse(T result)
        {
            Success = true;
            Result = result;
            Message = "Retrieve successful";
        }
        public void SuccessUpdateResponse()
        {
            Success = true;
            Message = "Update successful";
        }
        public void SuccessDeleteResponse()
        {
            Success = true;
            Message = "Delete successful";
        }
        public void TryCatchResponse(Exception ex)
        {
            Success = false;
            Message = ex.Message;
        }

    }
}
