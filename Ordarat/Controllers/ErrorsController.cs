using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.Errors;

namespace Ordarat.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)] 
    public class ErrorsController : BaseApiController
    {
        public ActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }

    }
}
