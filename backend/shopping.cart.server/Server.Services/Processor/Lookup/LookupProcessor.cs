using Microsoft.Extensions.Localization;
using Server.Core.BaseClasses;
using Server.Core.Manager;
using Server.Model.Dto;
using Server.Model.Dto.Lookup;
using Server.Model.Interfaces.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Processor.Lookup
{
    public class LookupProcessor : ProcessorBase<LookupRequest,List<LookupItem>>
    {
        public LookupProcessor(IRequestContext context) : base(context)
        {
        }

        public override ResponseBase<List<LookupItem>> DoProcess(LookupRequest request)
        {
            throw new NotImplementedException();
        }

        public override async Task<ResponseBase<List<LookupItem>>> DoProcessAsync(LookupRequest request)
        {
            List<LookupItem> response = null;
            List<ValidationError> errors = DoValidation(request);
            if (errors == null || errors.Count == 0)
            {
                response = LookupManager.Instance.Get(this.RequestContext, request.LookupEnum);
            }
            return new ResponseBase<List<LookupItem>>()
            {
                Result= response,
                Errors = errors,
                RoecordCount=response!=null && response.Count!=0?response.Count:0
            };
        }

        public override List<ValidationError> DoValidation(LookupRequest request)
        {
            List<ValidationError> errors = new();
            if (request == null || request.LookupEnum== Model.Enums.LookupsEnum.None)
            {
                errors.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("request_not_valid") });
            }
            return errors;
        }
    }
}
