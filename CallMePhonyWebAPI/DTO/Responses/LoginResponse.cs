using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.DTO.Responses
{
    public class LoginResponse : ResponseBase
    {
        public string? Token { get; set; }
        public User? User { get; set; }
    }
}
