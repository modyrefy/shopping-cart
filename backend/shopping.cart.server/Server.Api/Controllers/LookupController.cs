using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.BaseClasses;
using Server.Model.Dto.Lookup;
using Server.Model.Enums;
using Server.Model.Interfaces.Context;
using Server.Services.Processor.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Route("api/lookup")]
    //[ApiController]
    public class LookupController : ControllerBase
    {
        #region constructor
        private readonly IRequestContext _requestContext;
        public LookupController(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        #endregion
        #region get
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponseBase<List<LookupItem>>> Get(LookupsEnum enumValue, int? parentId=0, bool isRemovable = false, [System.Web.Http.FromUri] string[] idList = null)
        {
            LookupProcessor processor;
            using ((processor = new LookupProcessor(_requestContext)) as IDisposable)
            {
                return await processor.DoProcessAsync(new LookupRequest() 
                {
                    LookupEnum=enumValue ,
                    ParentId = parentId,
                    IsRemovable = isRemovable,
                    IdList = null,//idList != null && idList.Length != 0 ? idList.ToList() : null,
                });
            }
        }
        #endregion
    }
}
