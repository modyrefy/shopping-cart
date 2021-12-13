using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Request.Body.Peeker;
using Server.Core.BaseClasses;
using Server.Core.Interfaces.Repositories;
using Server.Model.Dto.Base;
using Server.Model.Dto.Exceptions;
using Server.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Common.Middleware
{
    //https://jasonwatmore.com/post/2020/10/02/aspnet-core-31-global-error-handler-tutorial
    //https://code-maze.com/global-error-handling-aspnetcore/
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            var requestReader = new StreamReader(context.Request.Body);
            var requestContent = requestReader.ReadToEndAsync();
            context.Request.Body.Position = 0;


            var responseReader = new StreamReader(context.Response.Body);
            var responseContent = responseReader.ReadToEndAsync();
            context.Response.Body.Position = 0;

            try
            {
                
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
               
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
               
               string errorCode= await  GenerateExceptionLog(context, error,  requestContent?.Result);

                ResponseBase<GeneralBaseClass> result = new()
                {
                    Errors = new List<Model.Dto.ValidationError>() {
                  new Model.Dto.ValidationError(){
                   ErrorMessage= error?.Message,
                   ErrorCode=errorCode
                  }}
                };
                // var result = JsonConvert.SerializeObject(new { message = error?.Message });
                await response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }

        //https://www.c-sharpcorner.com/article/save-request-and-response-headers-in-asp-net-5-core2/
        private async Task<string> GenerateExceptionLog(HttpContext context,Exception error,string requestEntity)
        {
            string hostname = Dns.GetHostName();
            var st = new System.Diagnostics.StackTrace(error, true);
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            var line = frame.GetFileLineNumber();

            List<string> allRequestHeaders = new();
            var headersKeyValuePair = context.Request.Headers.Where(x => allRequestHeaders.All(h => h != x.Key)).Select(x =>new KeyValuePair<string,string> (x.Key,x.Value));
            ExceptionLogs exceptionLogs = new() {
                ExceptionMessage = error.Message,
                ExceptionSource = error.Source,
                ExceptionDescription=frame.ToString(),
                ExceptionDetails= $"file: {frame.GetFileName()} line# {frame.GetFileLineNumber()}",
                ExceptionStackTrack = error.StackTrace,
            ControllerName = context.Request.RouteValues["controller"].ToString(),
                ActionName = context.Request.RouteValues["action"].ToString(),
                RequestHeader = JsonConvert.SerializeObject(headersKeyValuePair),
                 RequestObject = requestEntity,
                RequestMethodType = context.Request.Method.ToString(),
                RequestUrl= Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request),
                IpAddress = Dns.GetHostByName(hostname).AddressList.FirstOrDefault().ToString(),
                 HostName = hostname,
            DeviceName = Environment.MachineName,
        };
          var result=await  context.RequestServices.GetRequiredService<IRequestContext>().Repositories.ExceptionLogsRepository.InsertAsync(exceptionLogs);
          var finalResult=  await context.RequestServices.GetRequiredService<IRequestContext>().Repositories.ExceptionLogsRepository.SaveChangesAsync();
            return result?.ExceptionId.ToString();
        }
    }
}
