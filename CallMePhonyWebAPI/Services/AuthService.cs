using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CallMePhonyEntities.DTO.Requests;
using CallMePhonyEntities.DTO.Responses;
using CallMePhonyWebAPI.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace CallMePhonyWebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration, ILogger<AuthService> logger, IUserService userService)
        {
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
        }

        //</inheritdoc>
        public Task<LoginResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        //</inheritdoc>
        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var dbUser = await _userService.GetUserByMailAsync(email);

            if (dbUser != null)
            {
                if (PasswordHelper.VerifyPassword(password, dbUser.Password))
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new(ClaimTypes.Email, dbUser.Email),
                        new("UserType", UserTypeHelper.UserTypeToString(dbUser.UserType)),
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
