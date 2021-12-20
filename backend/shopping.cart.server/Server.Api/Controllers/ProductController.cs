using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.BaseClasses;
using Server.Model.Dto.Product;
using Server.Model.Interfaces.Context;
using Server.Services.Processor.Product;
using System;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region constructor
        private readonly IRequestContext _requestContext;
        public ProductController(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        #endregion
        #region post
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ResponseBase<ProductModel>> RegisterProduct([FromBody] ProductModel request)
        {
            RegisterProductProcessor processor;
            using ((processor = new RegisterProductProcessor(_requestContext)) as IDisposable)
            {
                return await processor.DoProcessAsync(request);
            }
        }
        #endregion
    }
}
