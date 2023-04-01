using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.Services
{
    public interface ISiteService
    {
        public Task<Site?> GetSiteAsync(int id);
        public Task<IEnumerable<Site?>> GetSitesByNameAsync(string name);
        public Task<IEnumerable<Site?>> GetSitesAsync();
        public Task<Site?> PostSiteAsync(Site model);
        public Task<Site?> PutSiteAsync(Site model);
        public Task<bool> DeleteSiteAsync(int id);

        public Task<bool> SiteExistsAsync(int id);
    }
}
