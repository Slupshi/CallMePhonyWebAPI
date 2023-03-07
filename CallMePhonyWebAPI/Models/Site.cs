using CallMePhonyWebAPI.Models.Enums;

namespace CallMePhonyWebAPI.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SiteType SiteType { get; set; }

        public virtual IEnumerable<Service> Services { get; set; }
    }
}
