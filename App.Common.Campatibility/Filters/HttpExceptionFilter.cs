using App.Common.Abstract.Helpers;
using App.Common.Campatibility.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace App.Common.Campatibility.Filters
{
    public class HttpExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<HttpExceptionFilter> _logger;

        public HttpExceptionFilter(ILogger<HttpExceptionFilter> logger)
        {
            _logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is ApplicationException fApiEx)
            {
                _logger.LogError(fApiEx, fApiEx.Message);
                var error = new ErrorModel(new List<string>() { fApiEx.Message });
                context.Result = new BadRequestObjectResult(error) { StatusCode = StatusCodes.Status400BadRequest };
            }
            else if (context.Exception is FrontendInformationException fInfEx)
            {
                context.HttpContext.ForceLogAsMaxInformation();
                var error = new ErrorModel(new List<string>() { fInfEx.Message });
                context.Result = new BadRequestObjectResult(error) { StatusCode = StatusCodes.Status400BadRequest };
            }
            else
            {
                var exception = context.Exception;
                _logger.LogError(exception, exception.Message);
                var error = new ErrorModel(new List<string>() { exception.Message });
                context.Result = new ObjectResult(error) { StatusCode = StatusCodes.Status500InternalServerError };
            }


            context.ExceptionHandled = true;
            return Task.FromResult(0);
        }
    }

    public class ErrorModel
    {
        public List<string> Messages { get; set; }

        public ErrorModel(List<string> messages = null)
        {
            if (messages != null)
            {
                Messages = messages;
            }
            else
            {
                Messages = new List<string>() { "An error occured." };
            }
        }
    }
}
