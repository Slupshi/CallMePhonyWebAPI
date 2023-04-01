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

        // </inheritedoc>
        public async Task<Site?> GetSiteAsync(int id) => await _context.Sites.Include(s => s.Users).FirstOrDefaultAsync(s => s.Id == id);

        // </inheritedoc>
        public async Task<Site?> GetSiteAsNoTrackingAsync(int id) => await _context.Sites.AsNoTracking().Include(s => s.Users).FirstOrDefaultAsync(s => s.Id == id);

        // </inheritedoc>
        public async Task<IEnumerable<Site?>> GetSitesAsync() => await _context.Sites.ToListAsync();

        // </inheritedoc>
        public async Task<IEnumerable<Site?>> GetSitesByNameAsync(string name) => await _context.Sites.Where(s => s.Name.Contains(name))
                                                                                                      .ToListAsync();
        // </inheritedoc>
        public async Task<Site?> PostSiteAsync(Site model)
        {
            Site? site = (await _context.Sites.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            return site;
        }

        // </inheritedoc>
        public async Task<Site?> PutSiteAsync(Site model)
        {
            if (model.Users != null)
            {
                List<User> users = new List<User>();
                foreach (var user in model.Users)
                {
                    users.Add(user);
                }
                model.Users = users;
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        // </inheritedoc>
        public async Task<bool> DeleteSiteAsync(int id)
        {
            Site? dbSite = await GetSiteAsync(id);
            if (dbSite != null)
            {
                _context.Sites.Remove(dbSite);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // </inheritedoc>
        public async Task<bool> SiteExistsAsync(int id)
        {
            Site? site = await _context.Sites.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (site != null)
            {
                return true;
            }
            return false;
        }
    }
}
