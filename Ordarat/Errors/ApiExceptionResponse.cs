namespace Ordarat.Errors
{
    public class ApiExceptionResponse:ApiResponse
    {
        public string Details { get; set; }

        public ApiExceptionResponse(int statusCode, string errorMessage = null , string details = null): base(statusCode, errorMessage) 
        {
            Details = details;

        }
    }
}
