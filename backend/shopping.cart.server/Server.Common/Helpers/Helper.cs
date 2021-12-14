using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Context;
using System.Linq;

namespace Server.Common.Helpers
{
    public class TokenContextHelper
    {
        #region instance
        private TokenContextHelper() { }
        private static TokenContextHelper instance = null;
        public static TokenContextHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TokenContextHelper();
                }
                return instance;
            }
        }
        #endregion
        public ActiveUserContext GetCurrentUserFromTokenContext(TokenValidatedContext context)
        {
            ActiveUserContext activeUserContext = null;
            //try
            //{
                var userId = context.Principal.Claims.Where(p => p.Type == "UserId").FirstOrDefault();
                if (userId != null && !string.IsNullOrEmpty(userId.Value))
                {
                    activeUserContext = context.HttpContext.RequestServices.GetRequiredService<IRequestContext>().DistributedCacheManager.GetOrSet<ActiveUserContext>($"user-{userId.Value}", null, null);
                }
            //}
            //catch (Exception ex)
            //{
            //    activeUserContext = null;
            //}
            return activeUserContext;
        }
        public string GetTokenValueFromTokenContext(TokenValidatedContext context)
        {
            string token = null;
            //try
            //{
                context.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authorizationToken);
                if (!string.IsNullOrEmpty(authorizationToken))
                {
                    token = authorizationToken.ToString().Replace("Bearer", "").Trim();
                }
            //}
            //catch (Exception ex)
            //{
            //     token = null;
            //}
            return token;
        }
    }
}
