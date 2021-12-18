using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.BaseClasses;
using Server.Model.Dto.Brand;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Context;
using Server.Services.Processor.Brand;
using Server.Services.Processor.User;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        #region constructor
        private readonly IRequestContext _requestContext;
        public BrandController(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        #endregion
        #region post
        [HttpPost]
        [Route("register")]
        public async Task<ResponseBase<BrandModel>> RegisterBrand([FromBody] BrandModel request)
        {
            RegisterBrandProcessor processor;
            using ((processor = new RegisterBrandProcessor(_requestContext)) as IDisposable)
            {
                return await processor.DoProcessAsync(request);
            }
        }
        #endregion
    }
}
