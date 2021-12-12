using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Server.Core.Interfaces.Repositories;
using Server.Model.Dto.User;
using Server.Services.Processor;
using System;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IRequestContext _requestContext;
        public AuthenticateController(IConfiguration config , IRequestContext requestContext)
        {
            _config = config;
            _requestContext = requestContext;
        }
        [AllowAnonymous]
        //[Route("authenticate")]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticateRequestModel login)
        {
            //IActionResult response = Unauthorized();
            //var user = AuthenticateUser(login);
            //_requestContext.ActiveUserContext = new ActiveUserContext() { UserId = 456, UserName = "xxxxx" };
            //if (user != null)
            //{
            //    var tokenString = GenerateJSONWebToken(user);
            //    response = Ok(new { token = tokenString });
            //}
            AuthincateUserProcessor processor;
            using ((processor = new AuthincateUserProcessor(_requestContext)) as IDisposable)
            {
                var result = await processor.DoProcessAsync(login);
                if (result.Errors != null && result.Errors.Count != 0)
                {
                    return Unauthorized(result);
                }
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            var p1 = _requestContext.ActiveUserContext;
            int x = 0;
            int y = 1;
            // int z = y / x;
            try
            {
                throw new Exception("general error occurred");
            }
            catch (Exception ex)
            { 
            
            }
            return Ok(Guid.NewGuid().ToString());
        }
    }
}
