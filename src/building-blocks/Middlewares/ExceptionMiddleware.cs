using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Context;
using System.Net;

namespace BuildingBlocks.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            string traceId = Guid.NewGuid().ToString();
            var problemDetails = new CustomProblemDetails();
            problemDetails.Message = exception.Message;
            problemDetails.Type = exception.GetType().Name.ToLower();
            problemDetails.TraceId = traceId;
            problemDetails.Instance = context.Request.Path;
            switch (exception)
            {
                case CustomException e:
                    problemDetails.Status = ((int)e.StatusCode);
                    break;

                case KeyNotFoundException:
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            LogContext.PushProperty("TraceId", traceId);
            LogContext.PushProperty("StackTrace", exception.StackTrace);
            Log.Error($"Request to {problemDetails.Instance} failed with Status Code : {problemDetails.Status}, Message : \"{problemDetails.Message}\" and Trace Id : \"{traceId}\".");

            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = problemDetails.Status.Value;
                string detailedResponse = JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
                await response.WriteAsync(detailedResponse);
            }
            else
            {
                Log.Warning("Can't write error response. Response has already started.");
            }
        }
    }
}
internal class CustomProblemDetails
{
    public string? Type { get; set; }
    public string? Message { get; set; }
    public int? Status { get; set; }
    public string? Instance { get; set; }
    public string? TraceId { get; set; }
}