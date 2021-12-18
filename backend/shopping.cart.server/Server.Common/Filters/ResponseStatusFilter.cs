using Microsoft.AspNetCore.Mvc.Filters;
using Server.Core.Interfaces.Base;
using System;
using System.Net;

namespace Server.Common.Filters
{
    //https://code-maze.com/action-filters-aspnetcore/
    [AttributeUsage(AttributeTargets.All)]
    public class ResponseStatusFilter : Attribute, IActionFilter//, IFilterMetadata, IAsyncActionFilter, IResultFilter, IAsyncResultFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                //var result = ((Microsoft.AspNetCore.Mvc.ObjectResult)context.Result).Value;
                var result = (Microsoft.AspNetCore.Mvc.ObjectResult)context.Result;
                if (result!=null && result.Value!=null && result.Value is IResponseBase @base)
                {
                    switch (context.HttpContext.Response.StatusCode)
                    {
                        case (int)HttpStatusCode.OK:
                            if (@base.Errors != null && @base.Errors.Count != 0)
                            {
                                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                ((Microsoft.AspNetCore.Mvc.ObjectResult)context.Result).StatusCode = (int)HttpStatusCode.BadRequest;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            { 
            
            }
        }
        public void OnActionExecuting(ActionExecutingContext context){}

        //public async  Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    // execute any code before the action executes
        //    var before = Guid.NewGuid();
        //    var result = await next();
        //    // execute any code after the action executes
        //    var after = Guid.NewGuid();
        //}
        //public void OnResultExecuted(ResultExecutedContext context)
        //{
        //    var after = Guid.NewGuid();
        //}
        //public void OnResultExecuting(ResultExecutingContext context)
        //{
        //    var after = Guid.NewGuid();
        //}
        //public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        //{
        //    var after = Guid.NewGuid();
        //}
    }
}
