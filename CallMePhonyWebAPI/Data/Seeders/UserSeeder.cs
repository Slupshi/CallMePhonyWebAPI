using CallMePhonyEntities.Models;
using CallMePhonyWebAPI.Helpers;

namespace CallMePhonyWebAPI.Data.Seeders
{
    public class UserSeeder
    {
        private static readonly Random _random = new Random();
        private static List<string> _firstNames = new()
        {
            "Julie", "Robert", "Rose", "Billy", "Pierre", "Sophie", "Anne", "Erick", "Stéphane", "Evan", "Théo", "Jean-Pierre",
        };

        private static List<string> _lastNames = new()
        {
            "Larousse", "Picolo", "Alto", "Deprato", "Sansnom", "JoliNom", "Bonom", "Padenom", "Rodriguez", "Alvarez", "Machin",
        };

        private static List<string> _genders = new()
        {
            "HOMME", "FEMME", "NON BINAIRE", "HELICOPTERE D'ATTAQUE",
        };

        private static string PhoneNumber()
        {
            string number = "0";
            while (number.Length < 10)
            {
                number += _random.Next(10);
            }
            return number;
        }

        public static void Seed(CallMePhonyDbContext context, int numberToSeed)
        {
            for (int i = 0; i < numberToSeed; i++)
            {
                if (context.Users.Count() < numberToSeed)
                {
                    string fn = _firstNames.ElementAt(_random.Next(_firstNames.Count));
                    fn = $"{char.ToUpper(fn[0])}{fn[1..]}"; // fn[1..] ---> fn.Substring(1);
                    string ln = _lastNames.ElementAt(_random.Next(_lastNames.Count)).ToUpper();
                    User user = new()
                    {
                        FirstName = fn,
                        LastName = ln,
                        UserName = $"{fn} {ln}",
                        Email = $"{fn.ToLower()}.{ln.ToLower()}{i}@annur.net",
                        Password = PasswordHelper.HashPassword(PasswordHelper.GeneratePassword()),
                        Gender = _genders.ElementAt(_random.Next(_genders.Count)).ToUpper(),
                        Phone = PhoneNumber(),
                        MobilePhone = PhoneNumber(),
                        UserType = CallMePhonyEntities.Enums.UserType.Visitor,
                        Service = context.Services.ToList().ElementAt(_random.Next(context.Services.Count())),
                        Site = context.Sites.ToList().ElementAt(_random.Next(context.Sites.Count())),
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
