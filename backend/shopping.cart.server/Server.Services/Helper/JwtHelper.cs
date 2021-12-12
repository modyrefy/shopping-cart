using Microsoft.IdentityModel.Tokens;
using Server.Model.Dto.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Services.Helper
{
    public class JwtHelper
    {
        #region instance
        private JwtHelper() { }
        private static JwtHelper instance = null;
        public static JwtHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JwtHelper();
                }
                return instance;
            }
        }
        #endregion
        public string GenerateJSONWebToken(List<KeyValuePair<string, string>> request, SettingConfiguration configuration) 
        {
            string token = null;
            if (request != null && request.Count != 0)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Jwt.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                List<Claim> claims = new();
                request.ForEach(row =>
                {
                    claims.Add(new Claim(row.Key, row.Value));
                });
                var jwtSecurityToken = new JwtSecurityToken(
                    configuration.Jwt.Issuer,
                    configuration.Jwt.Issuer,
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);
                token= new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            }
            return token;


        //    var claims = new[] {
        //          new Claim(ClaimTypes.Surname, userInfo.Username),
        //          new Claim("UserId", Guid.NewGuid().ToString()),
        //new Claim(ClaimTypes.Email, userInfo.EmailAddress),
        //new Claim(ClaimTypes.SerialNumber,JsonConvert.SerializeObject(userInfo)),
        //new Claim("DateOfJoing", DateTime.Now.AddDays(-300).ToString("yyyy-MM-dd")),
        //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username)
        //new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
        //new Claim(JwtRegisteredClaimNames.Acr,JsonConvert.SerializeObject(userInfo)),
        //new Claim("DateOfJoing", DateTime.Now.AddDays(-300).ToString("yyyy-MM-dd")),
        //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        }
    }
}
