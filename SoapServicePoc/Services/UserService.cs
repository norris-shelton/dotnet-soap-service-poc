using SoapServicePoc.Contracts;
using SoapServicePoc.Models;

namespace SoapServicePoc.Services
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", CreatedDate = DateTime.Now.AddDays(-30), IsActive = true },
            new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", CreatedDate = DateTime.Now.AddDays(-15), IsActive = true },
            new User { Id = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com", CreatedDate = DateTime.Now.AddDays(-7), IsActive = false }
        };

        private static int _nextId = 4;

        public Task<UserResponse> CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.FirstName) || 
                    string.IsNullOrWhiteSpace(request.LastName) || 
                    string.IsNullOrWhiteSpace(request.Email))
                {
                    return Task.FromResult(new UserResponse
                    {
                        Success = false,
                        Message = "First name, last name, and email are required."
                    });
                }

                // Check if email already exists
                if (_users.Any(u => u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    return Task.FromResult(new UserResponse
                    {
                        Success = false,
                        Message = "A user with this email already exists."
                    });
                }

                var newUser = new User
                {
                    Id = _nextId++,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                _users.Add(newUser);

                return Task.FromResult(new UserResponse
                {
                    Success = true,
                    Message = "User created successfully.",
                    User = newUser
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new UserResponse
                {
                    Success = false,
                    Message = $"Error creating user: {ex.Message}"
                });
            }
        }

        public Task<UserResponse> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Task.FromResult(new UserResponse
                    {
                        Success = false,
                        Message = "User not found."
                    });
                }

                return Task.FromResult(new UserResponse
                {
                    Success = true,
                    Message = "User retrieved successfully.",
                    User = user
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new UserResponse
                {
                    Success = false,
                    Message = $"Error retrieving user: {ex.Message}"
                });
            }
        }

        public Task<UsersListResponse> GetAllUsersAsync()
        {
            try
            {
                return Task.FromResult(new UsersListResponse
                {
                    Success = true,
                    Message = $"Retrieved {_users.Count} users successfully.",
                    Users = _users.ToList()
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new UsersListResponse
                {
                    Success = false,
                    Message = $"Error retrieving users: {ex.Message}",
                    Users = new List<User>()
                });
            }
        }

        public Task<UserResponse> UpdateUserAsync(User user)
        {
            try
            {
                var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser == null)
                {
                    return Task.FromResult(new UserResponse
                    {
                        Success = false,
                        Message = "User not found."
                    });
                }

                // Check if email is being changed and if it conflicts with another user
                if (!existingUser.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
                {
                    if (_users.Any(u => u.Id != user.Id && u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
                    {
                        return Task.FromResult(new UserResponse
                        {
                            Success = false,
                            Message = "A user with this email already exists."
                        });
                    }
                }

                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.IsActive = user.IsActive;

                return Task.FromResult(new UserResponse
                {
                    Success = true,
                    Message = "User updated successfully.",
                    User = existingUser
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new UserResponse
                {
                    Success = false,
                    Message = $"Error updating user: {ex.Message}"
                });
            }
        }

        public Task<UserResponse> DeleteUserAsync(int userId)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Task.FromResult(new UserResponse
                    {
                        Success = false,
                        Message = "User not found."
                    });
                }

                _users.Remove(user);

                return Task.FromResult(new UserResponse
                {
                    Success = true,
                    Message = "User deleted successfully.",
                    User = user
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new UserResponse
                {
                    Success = false,
                    Message = $"Error deleting user: {ex.Message}"
                });
            }
        }

        public Task<UserResponse> GetUserByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Task.FromResult(new UserResponse
                    {
                        Success = false,
                        Message = "Email is required."
                    });
                }

                var user = _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    return Task.FromResult(new UserResponse
                    {
                        Success = false,
                        Message = "User not found."
                    });
                }

                return Task.FromResult(new UserResponse
                {
                    Success = true,
                    Message = "User retrieved successfully.",
                    User = user
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new UserResponse
                {
                    Success = false,
                    Message = $"Error retrieving user: {ex.Message}"
                });
            }
        }
    }
}

