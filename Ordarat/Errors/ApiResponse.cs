using System;

namespace Ordarat.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse(int statusCode , string errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetDefaultStatusCodeMessage(statusCode);



        }

        private string GetDefaultStatusCodeMessage(int statusCode)
        => statusCode switch
        {
            400 => "A Bad Rquest You Have a Made",
            401 =>"Authorized , You are Not",
            404 => "Resourse was not found",
            500 => "Errors are The part to the dark side . Errors lead to anger. Anger lead to hate. Hate leads to carees change ",
            _ => null
              

        };
    }
}
