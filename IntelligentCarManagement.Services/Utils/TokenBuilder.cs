using IntelligentCarManagement.DataAccess.UnitsOfWork;
using Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Services.Utils;
using Microsoft.Extensions.Options;
using Models.Helpers;
using Api.Services.CustomExceptions;

namespace IntelligentCarManagement.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly UserManager<UserBase> _userManager;
        private readonly ApiSettings apiSettings;
        private const double EXPIRY_DURATION_HOURS = 72;

        public TokenBuilder(UserManager<UserBase> userManager, IOptions<ApiSettings> options)
        {
            _userManager = userManager;
            this.apiSettings = options.Value;
        }


        public async Task<string> BuildAsync(string email)
        {
            var signInCredentials = GetSigningCredentials();
            var claims = await GetClaims(email);

            var tokenOptions = new JwtSecurityToken(
                issuer: apiSettings.ValidIssuer,
                audience: apiSettings.ValidAudience,
                claims: claims,
                signingCredentials: signInCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        public bool Validate(string token)
        {
            var secretKey = Encoding.UTF8.GetBytes(apiSettings.SecretKey);
            var mySecurityKey = new SymmetricSecurityKey(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = apiSettings.ValidIssuer,
                    ValidAudience = apiSettings.ValidAudience,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSettings.SecretKey));

            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new NotFoundException("Invalid credentials.");

            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            return claims;
        }
    }
}
