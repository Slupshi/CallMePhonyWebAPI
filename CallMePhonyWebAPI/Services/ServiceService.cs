using CallMePhonyWebAPI.Data;
using CallMePhonyWebAPI.Models;
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

        public async Task<Service?> GetServiceAsync(int id) => await _context.Services.FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<Service?>> GetServicesAsync() => await _context.Services.ToListAsync();

        public async Task<IEnumerable<Service?>> GetServicesByNameAsync(string name) => await _context.Services.Where(s => s.Name.Contains(name))
                                                                                                               .ToListAsync();

        public async Task<Service?> PostServiceAsync(Service model)
        {
            Service? Service = (await _context.Services.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            return Service;
        }

        public async Task<Service?> PutServiceAsync(Service model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<bool> DeleteServiceAsync(int id)
        {
            Service? dbService = await GetServiceAsync(id);
            if (dbService != null)
            {
                _context.Services.Remove(dbService);
                return true;
            }
            return false;
        }

        public async Task<bool> ServiceExistsAsync(int id)
        {
            Service? service = await GetServiceAsync(id);
            if (service != null)
            {
                return true;
            }
            return false;
        }
    }
}
