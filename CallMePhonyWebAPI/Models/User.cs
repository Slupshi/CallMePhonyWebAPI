using CallMePhonyWebAPI.Models.Enums;

namespace CallMePhonyWebAPI.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string MobilePhone { get; set; }
    public string? Password { get; set; }
    public UserType? UserType { get; set; }

    public virtual Service? Service { get; set; }
    public virtual Site? Site { get; set; }
}
