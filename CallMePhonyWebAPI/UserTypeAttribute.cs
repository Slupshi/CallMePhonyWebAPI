using CallMePhonyEntities.Enums;
using CallMePhonyWebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CallMePhonyWebAPI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserTypeAttribute : Attribute, IAuthorizationFilter
    {
        public UserType UserType { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value == UserTypeHelper.UserTypeToString(UserType)))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
