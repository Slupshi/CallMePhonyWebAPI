using CallMePhonyEntities.Enums;

namespace CallMePhonyWebAPI.Helpers
{
    public class UserTypeHelper
    {
        public static string UserTypeToString(UserType? type)
        {
            return type switch
            {
                UserType.Visitor => Visitor,
                UserType.Admin => Admin,
                UserType.Maintenance => Maintenance,
                _ => Visitor
            };
        }

        public const string Visitor = "Visiteur";
        public const string Admin = "Administrateur";
        public const string Maintenance = "Maintenance";
    }
}
