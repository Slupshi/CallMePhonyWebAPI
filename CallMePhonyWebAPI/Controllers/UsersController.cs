using CallMePhonyWebAPI.DTO.Responses;
using CallMePhonyWebAPI.Helpers;
using CallMePhonyWebAPI.Models;
using CallMePhonyWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallMePhonyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
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
        /// <response code="200">Returns a UserResponse</response>
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

                UserResponse response = new();

                if (dbUser != null)
                {
                    response = new UserResponse(dbUser);
                    return StatusCode(StatusCodes.Status200OK, response);
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
        public async Task<ActionResult<UsersResponse>> GetUsersAsync()
        {
            try
            {
                IEnumerable<User?> dbUsers = await _userService.GetUsersAsync();
                if (dbUsers != null && dbUsers.Any())
                {
                    List<UserResponse> users = new();
                    foreach(var dbUser in dbUsers)
                    {
                        users.Add(new UserResponse(dbUser));
                    }
                    UsersResponse response = new()
                    {
                        Users = users,
                    };
                    return StatusCode(StatusCodes.Status200OK, response);
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
        [UserType(UserType = Models.Enums.UserType.Admin)]
        public async Task<ActionResult> PostUserAsync(User user)
        {
            try
            {
                if(user == null)
                {
                    return BadRequest("User can't be null");
                }

                // if password is not specify we will generate a random one
                string? temporaryPassword = null ;
                if(user.Password == null)
                {
                    temporaryPassword = PasswordHelper.GeneratePassword();
                    user.Password = temporaryPassword;
                }

                User? newUser = await _userService.PostUserAsync(user);

                if (newUser != null)
                {
                    UserResponse response = new UserResponse(newUser);
                    response.TemporaryPassword = temporaryPassword;
                    return StatusCode(StatusCodes.Status201Created, response);
                }
                else
                {
                    UserResponse response = new()
                    {
                        ErrorMessage = "Erreur dans la création de l'utilisateur",
                    };
                    return StatusCode(StatusCodes.Status204NoContent, response);
                }                
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
        [UserType(UserType = Models.Enums.UserType.Admin)]
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

                if (updatedUser != null)
                {
                    UserResponse response = new UserResponse(updatedUser);
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                else
                {
                    UserResponse response = new()
                    {
                        ErrorMessage = "Erreur dans la modification de l'utilisateur",
                    };
                    return StatusCode(StatusCodes.Status204NoContent, response);
                }
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
        [UserType(UserType = Models.Enums.UserType.Admin)]
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
