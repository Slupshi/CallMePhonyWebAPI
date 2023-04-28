using CallMePhonyEntities.Models;

namespace CallMePhonyWebAPI.Data.Seeders
{
    public class ServiceSeeder
    {
        private static readonly List<string> _names = new()
        {
            "Administration", "Comptabilité", "Maintenance", "Production", "Accueil", "Informatique", "Commercial", "Marketing",
        };

        public static void Seed(CallMePhonyDbContext context)
        {
            foreach (var name in _names)
            {
                if (!context.Services.Any(s => s.Name == name))
                {
                    Service service = new Service()
                    {
                        Name = name,
                    };
                    context.Services.Add(service);
                }
            }
            context.SaveChanges();
        }
    }
}
