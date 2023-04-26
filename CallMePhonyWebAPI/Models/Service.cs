namespace CallMePhonyWebAPI.Models
{
    public class Service : ModelBase, ISearchableObject
    {
        public string? Name { get; set; }

        public virtual IEnumerable<User>? Users { get; set; }
    }
}
