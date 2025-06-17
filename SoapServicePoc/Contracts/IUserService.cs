using System.ServiceModel;
using SoapServicePoc.Models;

namespace SoapServicePoc.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Task<UserResponse> CreateUserAsync(CreateUserRequest request);

        [OperationContract]
        Task<UserResponse> GetUserByIdAsync(int userId);

        [OperationContract]
        Task<UsersListResponse> GetAllUsersAsync();

        [OperationContract]
        Task<UserResponse> UpdateUserAsync(User user);

        [OperationContract]
        Task<UserResponse> DeleteUserAsync(int userId);

        [OperationContract]
        Task<UserResponse> GetUserByEmailAsync(string email);
    }
}

