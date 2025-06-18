using Microsoft.AspNetCore.Mvc;
using SoapServicePoc.Contracts;
using SoapServicePoc.Models;

namespace SoapServicePoc.Controllers
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
        /// Get all users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        public async Task<ActionResult<UsersListResponse>> GetAllUsers()
        {
            try
            {
                var result = await _userService.GetAllUsersAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UsersListResponse
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}",
                    Users = new List<User>()
                });
            }
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(id);
                if (!result.Success)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UserResponse
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">User email address</param>
        /// <returns>User details</returns>
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<UserResponse>> GetUserByEmail(string email)
        {
            try
            {
                var result = await _userService.GetUserByEmailAsync(email);
                if (!result.Success)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UserResponse
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="request">User creation request</param>
        /// <returns>Created user details</returns>
        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new UserResponse
                    {
                        Success = false,
                        Message = "Request body is required"
                    });
                }

                var result = await _userService.CreateUserAsync(request);
                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return CreatedAtAction(nameof(GetUserById), new { id = result.User?.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UserResponse
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="user">Updated user data</param>
        /// <returns>Updated user details</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(new UserResponse
                    {
                        Success = false,
                        Message = "Request body is required"
                    });
                }

                if (id != user.Id)
                {
                    return BadRequest(new UserResponse
                    {
                        Success = false,
                        Message = "ID in URL must match ID in request body"
                    });
                }

                var result = await _userService.UpdateUserAsync(user);
                if (!result.Success)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UserResponse
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserResponse>> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result.Success)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UserResponse
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }
    }
}

