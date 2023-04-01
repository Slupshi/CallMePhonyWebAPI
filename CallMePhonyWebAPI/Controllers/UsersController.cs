using CallMePhonyWebAPI.Models;
using CallMePhonyWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CallMePhonyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
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
        ///     GET /Users/1
        ///
        /// </remarks>
        /// <response code="200">Returns a User model</response>
        /// <response code="404">If the item doesn't exist</response>
        /// <response code="400">If the id is 0</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetUserAsync(int id)
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
        ///     GET /Users
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

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Users
        ///     
        /// </remarks>
        /// <response code="201">Returns the new User created</response>
        /// <response code="400">If the User model sent is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostUserAsync(User user)
        {
            try
            {
                if(user == null)
                {
                    return BadRequest("User can't be null");
                }

                User? newUser = await _userService.PostUserAsync(user);

                return StatusCode(StatusCodes.Status201Created, newUser);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        /// <summary>
        /// Update a User entity
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Users/1
        ///     
        /// </remarks>
        /// <response code="200">Returns the updated User model</response>
        /// <response code="400">If id equals 0</response>
        /// <response code="404">If the User doesn't exist</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUserAsync(int id, User user)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("ID must not be 0");
                }

                if (!await _userService.UserExistsAsync(id))
                {
                    return NotFound($"User with id: {id} not found");
                }

                User? updatedUser = await _userService.PutUserAsync(user);
                return StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        /// <summary>
        /// Delete a User entity
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Users/1
        ///     
        /// </remarks>
        /// <response code="200">Returns a boolean isDeleted</response>
        /// <response code="404">If the User doesn't exist</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            try
            {
                if (!await _userService.UserExistsAsync(id))
                {
                    return NotFound($"User with id: {id} not found");
                }

                bool isDeleted = await _userService.DeleteUserAsync(id);
                return StatusCode(StatusCodes.Status200OK, isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }
    }
}
