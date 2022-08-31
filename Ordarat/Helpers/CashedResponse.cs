using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Ordarat.Helpers
{
    public class CashedResponse : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSecond;

        public CashedResponse(int timeToLiveInSecond)
        {
            _timeToLiveInSecond = timeToLiveInSecond;
        }
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
