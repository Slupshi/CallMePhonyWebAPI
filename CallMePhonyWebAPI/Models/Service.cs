namespace CallMePhonyWebAPI.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
        public virtual Site Site { get; set; }
    }
}
