using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using filmsApi.DataAccess;
using filmsApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace filmsApi.Services
{
    public enum UserInfoResponse { Good, Invalid, DoesNotExist }
    public interface IUserInfoService
    {
        bool IsValidUserData(UserInfo userData);
        (string?, UserInfoResponse) GetTokenForUser(UserInfo userData);
        (UserInfo?, UserInfoResponse) VerifyUserAndPassword(string? email, string? password);
    }

    public class UserInfoService : IUserInfoService
    {
        public readonly IConfiguration _configuration;
        private readonly JwtContext _context;

        public UserInfoService(IConfiguration config, JwtContext context)
        {
            _configuration = config;
            _context = context;
        }

        public (string?, UserInfoResponse)  GetTokenForUser(UserInfo userData)
        {
            // See if the username(email) and password provided are correct against the UserInfo database table
            var (user, userInfoResponse) = VerifyUserAndPassword(userData?.Email, userData?.Password);

            // This case should not happen at this point unless db entries are broken
            if (user == null || userInfoResponse != UserInfoResponse.Good)
                return (null, userInfoResponse);

            return (new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                //create claims details based on the user information
                new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? string.Empty),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.DisplayName ?? string.Empty),
                        new Claim("Email", user.Email ?? string.Empty) },
                expires: DateTime.UtcNow.AddMinutes(1440),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty)), SecurityAlgorithms.HmacSha256))),
                userInfoResponse);
        }

        public bool IsValidUserData(UserInfo userData) => userData != null && userData.Email != null && userData.Password != null;

        public (UserInfo?, UserInfoResponse) VerifyUserAndPassword(string? email, string? password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return (null, UserInfoResponse.Invalid);
            
            var dbUser = _context?.UserInfos?.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (dbUser == null)
                return (null, UserInfoResponse.DoesNotExist);

            return (dbUser, UserInfoResponse.Good);
        }
    }
}