using CallMePhonyEntities.Models;

namespace CallMePhonyWebAPI.Data.Seeders
{
    public class SiteSeeder
    {
        private static readonly List<string> _cities = new()
        {
            "Paris", "Nantes", "Toulouse", "Nice", "Lille",
        };

        public static void Seed(CallMePhonyDbContext context)
        {
            foreach (var city in _cities)
            {
                if (!context.Sites.Any(s => s.Name.Contains(city)))
                {
                    Site site = new Site()
                    {
                        Name = $"Site de {city}",
                        SiteType = city == "Paris" ? CallMePhonyEntities.Enums.SiteType.Administration : CallMePhonyEntities.Enums.SiteType.Production,
                    };
                    context.Sites.Add(site);
                }
            }
            context.SaveChanges();
        }
    }
}
