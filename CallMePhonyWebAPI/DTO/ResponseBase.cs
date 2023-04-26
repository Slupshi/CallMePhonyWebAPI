using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.DTO
{
    public class ResponseBase : ModelBase
    {
        public string? ErrorMessage { get; set; }
    }
}
