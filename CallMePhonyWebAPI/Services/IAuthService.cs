using System.Security.Claims;
using CallMePhonyWebAPI.DTO.Requests;
using CallMePhonyWebAPI.DTO.Responses;

namespace CallMePhonyWebAPI.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="request">Request with user's information</param>
        /// <returns>LoginResponse with JWT Token and User model</returns>
        public Task<LoginResponse> RegisterAsync(RegisterRequest request);
        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>LoginResponse with JWT Token and User model</returns>
        public Task<LoginResponse> LoginAsync(string email, string password);
        /// <summary>
        /// Create a Json Web Token
        /// </summary>
        /// <param name="secret">Where to keep JWT</param>
        /// <param name="claims">Informations kept in the JWT</param>
        /// <returns></returns>
        public string CreateJWT(string secret, List<Claim> claims);
        /// <summary>
        /// Create a refresh Token
        /// </summary>
        /// <returns>Not implemented yet</returns>
        public string CreateRefreshToken();
    }
}
