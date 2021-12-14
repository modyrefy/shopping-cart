using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.BaseClasses;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Context;
using Server.Services.Processor.User;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region constructor
        private readonly IRequestContext _requestContext;
        public UserController(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        #endregion
        #region post
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ResponseBase<ActiveUserContext>> RegisterUser([FromBody] UserModel request)
        {
            RegisterUserProcessor processor;
            using ((processor = new RegisterUserProcessor(_requestContext)) as IDisposable)
            {
                var result = await processor.DoProcessAsync(request);
                Response.StatusCode = result.Errors != null && result.Errors.Count != 0 ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK ;
                return result;
            }
            // return null;
        }
        #endregion
    }
}
