using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Ordarat.BussniessLogicLayer.Interfaces;
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
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCashService>();
            var casheKey = GenerateCasheKeyFromRequest(context.HttpContext.Request);
            var cashedResponse = await cachedService.GetCashedResponse(casheKey);
            if (!string.IsNullOrEmpty(cashedResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cashedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var excutedEndPointContext =  await next();
            if(excutedEndPointContext.Result is OkObjectResult okObjectResult )
                await cachedService.CasheResponseAsync(casheKey , okObjectResult.Value , TimeSpan.FromSeconds(_timeToLiveInSecond));
        }

        private string GenerateCasheKeyFromRequest(HttpRequest request)
        {
            return "";
        }
    }
}
