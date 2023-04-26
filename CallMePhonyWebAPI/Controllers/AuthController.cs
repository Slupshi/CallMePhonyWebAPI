using CallMePhonyWebAPI.DTO.Requests;
using CallMePhonyWebAPI.DTO.Responses;
using CallMePhonyWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CallMePhonyWebAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Auth/login
        ///     
        /// </remarks>
        /// <response code="201">Returns a LoginResponse model</response>
        /// <response code="400">If the email or passowrd is empty</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LoginAsync(LoginRequest request)
        {
            if(request.Email != string.Empty && request.Password != string.Empty)
            {
                LoginResponse response = await _authService.LoginAsync(request.Email, request.Password);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            return BadRequest("Email and passoword must not be empty");            
        }

        /// <summary>
        /// Register [Not Implemented yet]
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Auth/register
        ///     
        /// </remarks>
        /// <response code="201">Returns a LoginResponse model</response>
        /// <response code="400"></response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Register(RegisterRequest request)
        {
            // No register only account creation with an Admin account
            throw new NotImplementedException();
        }
    }
}
