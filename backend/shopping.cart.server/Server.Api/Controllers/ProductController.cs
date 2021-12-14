using Microsoft.AspNetCore.Mvc;
using Server.Core.BaseClasses;
using Server.Model.Dto.Brand;
using Server.Model.Interfaces.Context;
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
        [Route("register")]
        public async Task<ResponseBase<BrandModel>> RegisterProduct([FromBody] BrandModel request)
        {
            return null;
        }
        #endregion
    }
}
