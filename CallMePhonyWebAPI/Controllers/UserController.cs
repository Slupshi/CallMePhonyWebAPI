using CallMePhonyWebAPI.Models;
using CallMePhonyWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallMePhonyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get a User by it's id
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /User/1
        ///
        /// </remarks>
        /// <response code="200">Returns a User model</response>
        /// <response code="404">If the item doesn't exist</response>
        /// <response code="400">If the id is 0</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("ID can't be 0");
                }

                User? dbUser = await _userService.GetUserAsync(id);
                if (dbUser != null)
                {
                    return StatusCode(StatusCodes.Status200OK, dbUser);
                }
                return StatusCode(StatusCodes.Status404NotFound, $"User with id: {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }

        }

        /// <summary>
        /// Get all User in a collection
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /User
        ///
        /// </remarks>
        /// <response code="200">Returns a User model collection</response>
        /// <response code="204">If there is no User</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                IEnumerable<User?> dbUsers = await _userService.GetUsersAsync();
                if (dbUsers != null && dbUsers.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, dbUsers);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }

        }
    }
}
