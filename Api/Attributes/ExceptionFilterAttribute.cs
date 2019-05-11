using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     This class is used to catch global exception on api calls
    /// </summary>
    public class ExceptionFilterAttribute : IExceptionFilter
    {
        /// <inheritdoc />
        /// <summary>
        ///     This method will be called when controller action
        ///     throw an exception
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new
            {
                context.Exception.Message,
                Exception = context.Exception.Demystify().ToString()
            });
        }
    }
}
