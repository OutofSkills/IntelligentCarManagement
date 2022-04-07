﻿using IntelligentCarManagement.DataAccess.UnitsOfWork;
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

namespace IntelligentCarManagement.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IUnitOfWork _repository;
        private readonly UserManager<UserBase> _userManager;
        private readonly SignInManager<UserBase> _signInManager;

        public TokenBuilder(IUnitOfWork unitOfWork, UserManager<UserBase> userManager, SignInManager<UserBase> signInManager)
        {
            _repository = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            UserBase user = await _userManager.FindByEmailAsync(username);
            var result = await _userManager.CheckPasswordAsync(user, password);

            return result;
        }

        public async Task<string> GenerateToken(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);

            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretSecurityKey.DontTouchIt")), SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
