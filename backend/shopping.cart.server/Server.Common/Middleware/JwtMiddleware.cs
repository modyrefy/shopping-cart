using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Common.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
       // private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
           // _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiSmlnbmVzaCBUcml2ZWRpIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9zZXJpYWxudW1iZXIiOiJ7XCJVc2VybmFtZVwiOlwiSmlnbmVzaCBUcml2ZWRpXCIsXCJQYXNzd29yZFwiOlwidGVzdC5idGVzdEBnbWFpbC5jb21cIn0iLCJEYXRlT2ZKb2luZyI6IjIwMjEtMDItMDkiLCJqdGkiOiI1ZmQ2ZDVlYi0xMzkzLTRjZDYtYjJkMC0wMzJmMGRjYWY0MjQiLCJleHAiOjE2Mzg3NTkxODcsImlzcyI6IlRlc3QuY29tIiwiYXVkIjoiVGVzdC5jb20ifQ.k71fLEdTsuzJh8XHjDFMra9w4NEnz_NZof_--RYImDY");

            await _next(context);
        }
        //https://jasonwatmore.com/post/2021/04/30/net-5-jwt-authentication-tutorial-with-example-api#jwt-middleware-cs
        private void attachUserToContext(HttpContext context,  string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("ThisismySecretKeyabc");//Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // attach user to context on successful jwt validation
                //context.Items["User"] = userService.GetById(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
