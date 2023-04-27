using CallMePhonyEntities.Models;
using CallMePhonyWebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CallMePhonyWebAPI.Services
{
    public class ServiceService : IServiceService
    {
        private readonly CallMePhonyDbContext _context;
        public ServiceService(CallMePhonyDbContext context)
        {
            _context = context;
        }

        // </inheritedoc>
        public async Task<Service?> GetServiceAsync(int id) => await _context.Services.Include(s => s.Users).FirstOrDefaultAsync(s => s.Id == id);

        // </inheritedoc>
        public async Task<Service?> GetServiceAsNoTrackingAsync(int id) => await _context.Services.AsNoTracking().Include(s => s.Users).FirstOrDefaultAsync(s => s.Id == id);

        // </inheritedoc>
        public async Task<IEnumerable<Service?>> GetServicesAsync() => await _context.Services.ToListAsync();

        // </inheritedoc>
        public async Task<IEnumerable<Service?>> GetServicesByNameAsync(string name) => await _context.Services.Where(s => s.Name.Contains(name))
                                                                                                               .ToListAsync();
        // </inheritedoc>
        public async Task<Service?> PostServiceAsync(Service model)
        {
            model.Name = model.Name.Trim();

            Service? service = (await _context.Services.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            return service;
        }

        // </inheritedoc>
        public async Task<Service?> PutServiceAsync(Service model)
        {
            model.Name = model.Name.Trim();

            if (model.Users != null)
            {
                List<User> users = new List<User>();
                foreach (var user in model.Users)
                {
                    users.Add(user);
                }
                model.Users = users;
            }

            model.CreatedAt = (await GetServiceAsNoTrackingAsync(model.Id))?.CreatedAt;
            model.UpdatedAt = DateTime.Now;
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        // </inheritedoc>
        public async Task<bool> DeleteServiceAsync(int id)
        {
            Service? dbService = await GetServiceAsync(id);
            if (dbService != null)
            {
                _context.Services.Remove(dbService);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // </inheritedoc>
        public async Task<bool> ServiceExistsAsync(int id)
        {
            Service? service = await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (service != null)
            {
                return true;
            }
            return false;
        }
    }
}
