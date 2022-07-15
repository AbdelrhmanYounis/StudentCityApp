using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using StudentCity.Application.AppService;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;
using StudentCity.DAL.Shared;
using StudentCityUI.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace StudentCityUI.Security
{
    public class SecurityManager
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserRepository _appService;
        public SecurityManager(JwtSettings jwtSettings, UserRepository servciAppService)
        {
            _jwtSettings = jwtSettings;
            _appService = servciAppService;
        }


        public ClaimsIdentity ValidateUser(UserModel user)
        {
           
            if (user != null && !user.IsDisabled)
            {
                var claims_identity = new ClaimsIdentity();

                claims_identity.AddClaims(new List<Claim>
                {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.GivenName, (user.FirstName+ " "+user.LastName)),
                        new Claim(ClaimTypes.NameIdentifier, user.Email),
                        new Claim(ClaimTypes.Name,(user.FirstName+ " "+user.LastName))
                 });

                //    ,
                //new Claim("branchId", branchId)
                //Dictionary<string, string> data = new Dictionary<string, string>
                //{
                //    { "roles" , string.Join("|" , user.Roles.Select(r =>r .RoleModel).Select(r => r.Name)) }
                //};


                return claims_identity;
            }

            return null;

        }




        public string BuildJwtToken(ClaimsIdentity claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims.Claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(10),

                signingCredentials: new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256));


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}