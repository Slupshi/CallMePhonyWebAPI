using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.DTO.Responses
{
    public class LoginResponse : ResponseBase
    {
        public string? Token { get; set; }
        public UserResponse? User { get; set; }
    }
}
