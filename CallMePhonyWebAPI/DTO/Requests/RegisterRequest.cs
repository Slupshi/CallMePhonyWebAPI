using CallMePhonyWebAPI.Models.Enums;
using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.DTO.Requests
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Password { get; set; }
    }
}
