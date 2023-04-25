using CallMePhonyWebAPI.Models.Enums;
using CallMePhonyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CallMePhonyWebAPI.DTO.Responses
{
    public class UserResponse : ResponseBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public UserType? UserType { get; set; }
        public int? ServiceId { get; set; }
        public int? SiteId { get; set; }

        public UserResponse() { }

        public UserResponse(User user) 
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Email = user.Email;
            UserType = user.UserType;
            Phone = user.Phone;
            MobilePhone = user.MobilePhone;
            ServiceId = user.Service?.Id;
            SiteId = user.Site?.Id;
        }

    }
}
