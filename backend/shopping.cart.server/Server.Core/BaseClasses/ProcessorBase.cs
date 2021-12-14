using Microsoft.Extensions.Localization;
using Server.Model.Dto;
using Server.Model.Interfaces.Context;
using Server.Resources.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Core.BaseClasses
{
    public abstract class ProcessorBase<Request, Response>
        where Request : class
        where Response : class
    {
        public readonly IRequestContext RequestContext;
        public readonly IStringLocalizer<ValidationMessageResource> ValidationMessages;
        protected ProcessorBase(IRequestContext context)
        { 
            this.RequestContext= context;
            this.ValidationMessages = RequestContext.GetLocalResource<ValidationMessageResource>();
        }
        public abstract List<ValidationError> DoValidation(Request request);
        public abstract ResponseBase<Response> DoProcess(Request request);
        public abstract  Task<ResponseBase<Response>> DoProcessAsync(Request request);

    }
}
