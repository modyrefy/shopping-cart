using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Server.Core.BaseClasses;
using Server.Model.Dto.Base;
using Server.Model.Dto.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
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
                ResponseBase<GeneralBaseClass> result = new()
                {
                    Errors = new List<Model.Dto.ValidationError>() {
                  new Model.Dto.ValidationError(){
                   ErrorMessage= error?.Message
                  }
                 }
                };
               // var result = JsonConvert.SerializeObject(new { message = error?.Message });
                await response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
