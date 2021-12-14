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
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region constructor
        private readonly IRequestContext _requestContext;
        public AuthenticateController(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        #endregion
        #region post
        [AllowAnonymous]
        [HttpPost]
        public async Task<ResponseBase<ActiveUserContext>> Authenticate([FromBody] UserAuthenticateRequestModel login)
        {
            //IActionResult response = Unauthorized();
            //var user = AuthenticateUser(login);
            //_requestContext.ActiveUserContext = new ActiveUserContext() { UserId = 456, UserName = "xxxxx" };
            //if (user != null)
            //{
            //    var tokenString = GenerateJSONWebToken(user);
            //    response = Ok(new { token = tokenString });
            //}
            // throw new System.Exception("general error occurred");
            AuthincateUserProcessor processor;
            using ((processor = new AuthincateUserProcessor(_requestContext)) as IDisposable)
            {
                var result = await processor.DoProcessAsync(login);
                Response.StatusCode = result.Errors != null && result.Errors.Count != 0 ? (int)HttpStatusCode.Unauthorized : (int)HttpStatusCode.OK;
                return result;
                //if (result.Errors != null && result.Errors.Count != 0)
                //{
                //    return Unauthorized(result);
                //}
                //return Ok(result);
            }
        }

       
        #endregion
        #region get
        //[HttpGet]
        //[Route("test")]
        //public IActionResult Test()
        //{
        //    var p1 = _requestContext.ActiveUserContext;
        //    int x = 0;
        //    int y = 1;
        //    // int z = y / x;
        //        throw new Exception("general error occurred");
        //    return Ok(Guid.NewGuid().ToString());
        //}
        #endregion
    }
}
