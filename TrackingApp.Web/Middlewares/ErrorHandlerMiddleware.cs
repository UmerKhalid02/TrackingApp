using Microsoft.AspNetCore.Http.Extensions;
using System.Net;
using TrackingApp.Application.DataTransferObjects.Shared;
using NLog;
using System.Text.Json;
using TrackingApp.Application.Wrappers;
using TrackingApp.Application.Exceptions;

namespace TrackingApp.Web.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Logger logger;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            logger = LogManager.GetLogger("RequestResponseErrorHandlerMiddleware");
        }

        public async Task Invoke(HttpContext context)
        {
            IServiceProvider services = context.RequestServices;
            NLogTrack logTrack = services.GetService(typeof(NLogTrack)) as NLogTrack;

            try
            {
                logTrack.TraceIdentifier = context.TraceIdentifier;
                string endpoint = context.Request.GetDisplayUrl();
                string methodType = context.Request.Method;

                string value = $"Start:Type:{methodType} EndPoint:{endpoint}";
                logTrack.LogAccess(value);
                await _next(context);
                logTrack.LogAccess("API call Ends.");
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ErrorResponse<string>() { success = false, message = error?.Message };

                switch (error)
                {
                    case ApiException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        responseModel.errors = e.errorModels;
                        break;

                    case ConflictException e:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        responseModel.errors = e.ErrorModels;
                        break;

                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        responseModel.errors = e.Errors;
                        break;

                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    /*case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;*/
                    default:
                        if (error?.Message.ToLower().Contains("No authenticationScheme was specified".ToLower()) == true)
                        {
                            response.StatusCode = (int)HttpStatusCode.Forbidden;
                            responseModel.message = "You don't have privileges to perform this action!";
                        }
                        else
                        {
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        }

                        break;
                }
                logTrack.LogAccess($"StatusCode:{response.StatusCode} Error:{error?.Message}");
                logTrack.LogAccess($"StackTrace:{error?.StackTrace}");
                logTrack.LogAccess("API call Ends with exception.");
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
