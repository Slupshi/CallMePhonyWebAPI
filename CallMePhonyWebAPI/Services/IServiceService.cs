using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.Services
{
    public interface IServiceService
    {
        public Task<Service?> GetServiceAsync(int id);
        public Task<IEnumerable<Service?>> GetServicesByNameAsync(string name);
        public Task<IEnumerable<Service?>> GetServicesAsync();
        public Task<Service?> PostServiceAsync(Service model);
        public Task<Service?> PutServiceAsync(Service model);
        public Task<bool> DeleteServiceAsync(int id);
    }
}
