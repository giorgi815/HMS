namespace HMS
{
    public class CommonResponse
    {
        public CommonResponse()
        {
        }

        public CommonResponse(string message, object result, bool isSuccess, int httpStatusCode)
        {
            Message = message;
            Result = result;
            IsSuccess = isSuccess;
            HttpStatusCode = httpStatusCode;
        }

        public string Message { get; set; }
        public object Result { get; set; }
        public bool IsSuccess { get; set; }
        public int HttpStatusCode { get; set; }
    }

    public static class CommonResponseMessage
    {
        public static string SuccessMessage { get; } = "Request processed successfully.";
    }

}
