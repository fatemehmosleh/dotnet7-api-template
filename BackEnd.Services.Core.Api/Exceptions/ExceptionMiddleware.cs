using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Namotion.Reflection;
using BackEnd.Common.Core.Exceptions;

namespace BackEnd.Services.Core.Api.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";


            if (exception is IApplicationException || exception is IDomainException ||
                exception is IPresentationException)
                context.Response.StatusCode = (int) HttpStatusCode.Conflict;
            else

                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;


            var message = "";

            if (exception.Source != null && exception.Source.Equals("BackEnd.Common.ThingsBoardRestApis"))
            {
                var content = exception.TryGetPropertyValue<string>("ErrorContent");

                if (content != null)
                {
                    var resolvedContent = JsonSerializer.Deserialize<JsonDocument>(content);
                    message = resolvedContent?.RootElement.GetProperty("message").ToString();
                }
            }

            return context.Response.WriteAsync(new ErrorDetails
            {
                HResult = exception.HResult,
                StatusCode = context.Response.StatusCode,
                Message = !string.IsNullOrEmpty(message)
                    ? message
                    : $"Internal Server Error. {exception.Message}"
            }.ToString());
        }
    }
}