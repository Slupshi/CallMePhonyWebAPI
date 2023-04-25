using CallMePhonyWebAPI.DTO.Requests;
using CallMePhonyWebAPI.DTO.Responses;
using CallMePhonyWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallMePhonyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger,IAuthService authService)
        {
            _logger = logger;
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
        public ActionResult Login(LoginRequest request)
        {
            if(request.Email != string.Empty && request.Password != string.Empty)
            {
                LoginResponse response = _authService.Login(request.Email, request.Password);
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
