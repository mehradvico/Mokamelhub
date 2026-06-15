using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Common.Helpers;
using Application.Services.Accounting.UserTokenSrv.Dto;
using Application.Services.Accounting.UserTokenSrv.Iface;
using AutoMapper;
using Entities.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Accounting.UserTokenSrv.Srv
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UserTokenService(IDataBaseContext _context, IConfiguration configuration, IMapper mapper)
        {
            this._context = _context;
            this.configuration = configuration;
            this.mapper = mapper;
        }
        public UserTokenDto CreateToken(User user, bool isAdmin = false)
        {
            var userToken = CreateUserTokenDto(user, isAdmin);

            var claims = new List<Claim>
                {
                    new Claim ("UserId", userToken.UserId.ToString()),
                    new Claim ("FirstName",  userToken.FirstName??" "),
                    new Claim ("LastName",  userToken.LastName??" "),
                    new Claim ("RoleId",  userToken.RoleId.ToString()),
                };
            string key = userToken.JwtKey;
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenexp = DateTime.UtcNow.AddMinutes(int.Parse(userToken.TokenExpires));
            var token = new JwtSecurityToken(
                issuer: userToken.Issuer,
                audience: userToken.Audience,
                expires: tokenexp,
                notBefore: DateTime.UtcNow,
                claims: claims,
                signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = Guid.NewGuid().ToString();
            var item = mapper.Map<UserToken>(userToken);
            item.TokenExp = DateTime.UtcNow.AddMinutes(Convert.ToInt32(userToken.TokenExpires));
            item.RefreshTokenExp = DateTime.UtcNow.AddMinutes(Convert.ToInt32(userToken.RefreshExpires));
            item.CreateDate = DateTime.UtcNow;
            item.Deleted = false;
            item.RefreshTokenHash = refreshToken.Tosha256Hash();
            item.TokenHash = jwtToken.Tosha256Hash();
            _context.UserTokens.Add(item);
            _context.SaveChanges();
            var result = new UserTokenDto()
            {
                UserId = userToken.UserId,
                RefreshToken = refreshToken,
                Token = jwtToken,
                FirstName = userToken.FirstName,
                LastName = userToken.LastName,
                TokenExp = item.TokenExp,
                RefreshTokenExp = item.RefreshTokenExp
            };
            return result;

        }

        public async Task<BaseResultDto> RefreshTokenAsync(RefreshTokenDto refreshToken)
        {
            var hashedToken = refreshToken.Token.Tosha256Hash();
            var hashedRefreshToken = refreshToken.RefreshToken.Tosha256Hash();
            var userToken = await _context.UserTokens.Include(s => s.User).ThenInclude(s => s.Role).FirstOrDefaultAsync(s => s.TokenHash.Equals(hashedToken) && s.RefreshTokenHash == hashedRefreshToken && s.RefreshTokenExp > DateTime.UtcNow && s.Deleted != true);
            if (userToken != null)
            {
                var createdDto = CreateToken(userToken.User, refreshToken.IsAdmin);
                return new BaseResultDto<UserTokenDto>(true, createdDto);
            }
            return new BaseResultDto(false, val: Resource.Notification.TokenExpired);
        }
        private CreateUserTokenDto CreateUserTokenDto(User user, bool isAdmin = false)
        {
            var createToken = new CreateUserTokenDto()
            {
                UserId = user.Id,
                RoleId = user.RoleId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Issuer = configuration["JWtConfig:issuer"],
                Audience = configuration["JWtConfig:audience"],
                JwtKey = configuration["JWtConfig:key"],
                TokenExpires = user.Role.Label == RoleEnum.Admin.ToString() && isAdmin ? configuration["JWtConfig:expires_admin_minutes"] : configuration["JWtConfig:expires_user_minutes"],
                RefreshExpires = user.Role.Label == RoleEnum.Admin.ToString() && isAdmin ? configuration["JWtConfig:refresh_expires_admin_minutes"] : configuration["JWtConfig:refresh_expires_user_minutes"],
                Provider = "windows",
                DeviceName = "zand"
            };
            return createToken;
        }
        public async Task<BaseResultDto> SignOut(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var hashed = token.Tosha256Hash();
                var userToken = await _context.UserTokens.FirstOrDefaultAsync(x => x.TokenHash == hashed);
                if (userToken != null)
                {
                    userToken.Deleted = true;
                    _context.UserTokens.Update(userToken);
                    _context.SaveChanges();
                    return new BaseResultDto(isSuccess: true);
                }
            }
            return new BaseResultDto(isSuccess: false, Resource.Notification.InvalidToken);
        }


        public async Task<BaseResultDto> ResetTokenAsync(User user, bool isAdmin = false)
        {
            var userTokens = await _context.UserTokens.Where(x => x.UserId == user.Id).ExecuteUpdateAsync(s => s.SetProperty(p => p.Deleted, true));
            var newToken = CreateToken(user, isAdmin);
            return new BaseResultDto<UserTokenDto>(isSuccess: true, newToken);
        }
    }
}
