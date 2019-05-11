using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Api.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     This class is used to catch global exception on api calls
    /// </summary>
    public class ExceptionFilterAttribute : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilterAttribute> _logger;
        public ExceptionFilterAttribute(ILogger<ExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        
        /// <inheritdoc />
        /// <summary>
        ///     This method will be called when controller action
        ///     throw an exception
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var demystifiedException = context.Exception.Demystify();
            
            _logger.LogError(demystifiedException, "Exception caught by ExceptionFilterAttribute");

            context.Result = new BadRequestObjectResult(new
            {
                context.Exception.Message,
                Exception = demystifiedException.ToString()
            });
        }
    }
}
