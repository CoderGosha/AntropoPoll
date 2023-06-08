using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AntropoPollWebApi.Core.Services
{
    public class TokenService
    {
        private readonly AuthSettings _authSettings;

        public TokenService(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }


        public string GetAccessToken(UserClaims user)
        {
            //Создаем токен доступа
            var identity = GetIdentity(user);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: "Antropo",
                audience: "http://localhost",
                notBefore: now,
                claims: identity.Claims,
                expires: null,
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.Key));
        }

        private ClaimsIdentity GetIdentity(UserClaims userClaims)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userClaims.Guid.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"),
                new Claim("UserClaims", JsonConvert.SerializeObject(userClaims)),
            };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        internal UserClaims GetUserClaims(ClaimsPrincipal user)
        {
            var userClaimsObject = user.Claims.Where(x => x.Type == "UserClaims").FirstOrDefault();
            if (userClaimsObject == null)
                return null;
            try
            {
                var userClaims = JsonConvert.DeserializeObject<UserClaims>(userClaimsObject.Value);
                return userClaims;
            }

            catch (Exception)
            {
                return null;
            }
        }

    }
}
