using CallMePhonyWebAPI.Data;
using CallMePhonyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CallMePhonyWebAPI.Services
{
    public class SiteService : ISiteService
    {
        private readonly CallMePhonyDbContext _context;
        public SiteService(CallMePhonyDbContext context) 
        {
            _context = context;
        }

        public async Task<Site?> GetSiteAsync(int id) => await _context.Sites.FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<Site?>> GetSitesAsync() => await _context.Sites.ToListAsync();

        public async Task<IEnumerable<Site?>> GetSitesByNameAsync(string name) => await _context.Sites.Where(s => s.Name.Contains(name))
                                                                                                      .ToListAsync();

        public async Task<Site?> PostSiteAsync(Site model)
        {
            Site? site = (await _context.Sites.AddAsync(model)).Entity;
            return site;
        }

        public async Task<Site?> PutSiteAsync(Site model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<bool> DeleteSiteAsync(int id)
        {
            Site? dbSite = await GetSiteAsync(id);
            if (dbSite != null)
            {
                _context.Sites.Remove(dbSite);
                return true;
            }
            return false;
        }
    }
}
