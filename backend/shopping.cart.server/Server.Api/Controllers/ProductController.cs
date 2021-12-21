using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.BaseClasses;
using Server.Model.Dto.Product;
using Server.Model.Interfaces.Context;
using Server.Services.Processor.Product;
using System;
using System.Collections.Generic;
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
        //[AllowAnonymous]
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
        #region get
        [HttpGet]
        [AllowAnonymous]
        [Route("dashboard")]
        public async Task<ResponseBase<List<ProductDashboardModelResponse>>> SearchProductDashBoard(string name = null, int brandId = 0, int categoryId = 0, int pageIndex = 0, int pageSize = 10)
        {
            SearchProductDashboardProcessor processor;
            using ((processor = new SearchProductDashboardProcessor(_requestContext)) as IDisposable)
            {
                return await processor.DoProcessAsync(new ProductDashboardModelRequest()
                {
                    Name = name,
                    CategoryId = categoryId,
                    BrandId = brandId,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });
            }
        }
        #endregion
    }
}
