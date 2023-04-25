using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CallMePhonyWebAPI.Data;
using CallMePhonyWebAPI.DTO.Requests;
using CallMePhonyWebAPI.DTO.Responses;
using CallMePhonyWebAPI.Helpers;
using CallMePhonyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CallMePhonyWebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly CallMePhonyDbContext _context;

        public AuthService(IConfiguration configuration, ILogger<AuthService> logger, CallMePhonyDbContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        //</inheritdoc>
        public Task<LoginResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        //</inheritdoc>
        public LoginResponse Login(string email, string password)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Email == email);

            if (dbUser != null)
            {
                if(PasswordHelper.VerifyPassword(password, dbUser.Password))
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new(ClaimTypes.Email, dbUser.Email),
                    };

                    LoginResponse response = new()
                    {
                        User = new UserResponse(dbUser),
                        Token = CreateJWT(_configuration["Jwt:Key"]!, claims),
                    };
                    return response;
                }
                return new LoginResponse()
                {
                    ErrorMessage = $"Password incorrect",
                };
            }   
            return new LoginResponse() 
            {
                ErrorMessage = $"No User with email : {email} exists",
            };
        }

        //</inheritdoc>
        public string CreateJWT(string secret, List<Claim> claims)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _configuration["Jwt:Issuer"],
            };

            var securityToken = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }

        //</inheritdoc>
        public string CreateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
