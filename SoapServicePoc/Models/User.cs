using System.Runtime.Serialization;

namespace SoapServicePoc.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; } = string.Empty;

        [DataMember]
        public string LastName { get; set; } = string.Empty;

        [DataMember]
        public string Email { get; set; } = string.Empty;

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }

    [DataContract]
    public class CreateUserRequest
    {
        [DataMember]
        public string FirstName { get; set; } = string.Empty;

        [DataMember]
        public string LastName { get; set; } = string.Empty;

        [DataMember]
        public string Email { get; set; } = string.Empty;
    }

    [DataContract]
    public class UserResponse
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string Message { get; set; } = string.Empty;

        [DataMember]
        public User? User { get; set; }
    }

    [DataContract]
    public class UsersListResponse
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string Message { get; set; } = string.Empty;

        [DataMember]
        public List<User> Users { get; set; } = new List<User>();
    }
}

