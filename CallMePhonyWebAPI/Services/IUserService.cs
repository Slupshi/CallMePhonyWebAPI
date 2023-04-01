using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.Services
{
    public interface IUserService
    {
        public Task<User?> GetUserAsync(int id);
        public Task<IEnumerable<User?>> GetUsersByNameAsync(string username);
        public Task<IEnumerable<User?>> GetUsersBySiteAsync(Site site);
        public Task<IEnumerable<User?>> GetUsersByServiceAsync(Service service);
        public Task<User?> GetUserByMailAsync(string email);
        public Task<IEnumerable<User?>> GetUsersAsync();
        public Task<User?> PostUserAsync(User model);
        public Task<User?> PutUserAsync(User model);
        public Task<bool> DeleteUserAsync(int id);

        public Task<bool> UserExistsAsync(int id);
    }
}
