namespace CallMePhonyWebAPI.Helpers
{
    public class PasswordHelper
    {
        private static Random _random = new Random();

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
        }


        public static string GeneratePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789&#%$£!çàéè";
            string password = new string(Enumerable.Repeat(chars, 32)
                                        .Select(s => s[_random.Next(s.Length)])
                                        .ToArray());
            return HashPassword(password);
        }
    }
}
