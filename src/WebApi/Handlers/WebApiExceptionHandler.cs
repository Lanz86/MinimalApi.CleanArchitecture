using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinimalApi.CleanArchitecture.Application.Common.Exceptions;
using System.Net;

namespace MinimalApi.CleanArchitecture.WebApi.Handlers
{
    internal class WebApiExceptionHandler 
    {
        private readonly IDictionary<Type, Func<Exception, ObjectResult>> _exceptionHandlers;

        public WebApiExceptionHandler()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Func<Exception, ObjectResult>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
        }

        public ObjectResult HandleException(Exception exception)
        {
            Type type = exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                return _exceptionHandlers[type].Invoke(exception);
            }

            return HandleServerErrorException(exception);
        }

        private ObjectResult HandleServerErrorException(Exception exception)
        {
            var ex = exception;
            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Internal server error.",
                Detail = exception.Message
            };

            return new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        private ObjectResult HandleValidationException(Exception exception)
        {
            var ex = (ValidationException)exception;

            var details = new ValidationProblemDetails(ex.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            return new BadRequestObjectResult(details);
        }

        //private void HandleInvalidModelStateException(Exception context)
        //{
        //    var details = new ValidationProblemDetails(context.ModelState)
        //    {
        //        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        //    };

        //    context.Result = new BadRequestObjectResult(details);

        //    context.ExceptionHandled = true;
        //}

        private ObjectResult HandleNotFoundException(Exception exception)
        {
            var ex = (NotFoundException)exception;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            return new NotFoundObjectResult(details);
        }

        private ObjectResult HandleUnauthorizedAccessException(Exception exception)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };

            return new ObjectResult(details) 
            { 
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        private ObjectResult HandleForbiddenAccessException(Exception context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            return new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }

    public static class WebApiExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseWebApiExceptionHandler(this IApplicationBuilder app, ILogger? logger = null, bool logStructuredException = false)
        {
            app.UseExceptionHandler(errApp =>
            {
                errApp.Run(async ctx =>
                {
                    var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                    if (exHandlerFeature is not null)
                    {
                        logger ??= ctx.Resolve<ILogger<WebApiExceptionHandler>>();
                        var http = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
                        var type = exHandlerFeature.Error.GetType().Name;
                        var error = exHandlerFeature.Error.Message;
                        var msg =
    $@"================================= 
{http} 
TYPE: {type} 
REASON: {error} 
--------------------------------- 
{exHandlerFeature.Error.StackTrace}";

                        if (logStructuredException)
                            logger.LogError("{@http}{@type}{@reason}{@exception}", http, type, error, exHandlerFeature.Error);
                        else
                            logger.LogError(msg);

                        var objresult = new WebApiExceptionHandler().HandleException(exHandlerFeature.Error);

                        ctx.Response.StatusCode = objresult.StatusCode??500;
                        ctx.Response.ContentType = "application/problem+json";
                        await ctx.Response.WriteAsJsonAsync(objresult.Value);
                    }
                });
            });

            return app;
        }
    }
}
