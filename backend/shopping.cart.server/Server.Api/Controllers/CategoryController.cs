using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.BaseClasses;
using Server.Model.Dto.Category;
using Server.Model.Interfaces.Context;
using Server.Services.Processor.Category;
using System;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region constructor
        private readonly IRequestContext _requestContext;
        public CategoryController(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        #endregion
        #region post
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ResponseBase<CategoryModel>> RegisterBrand([FromBody] CategoryModel request)
        {
            RegisterCategoryProcessor processor;
            using ((processor = new RegisterCategoryProcessor(_requestContext)) as IDisposable)
            {
                return await processor.DoProcessAsync(request);
            }
        }
        #endregion
    }
}
