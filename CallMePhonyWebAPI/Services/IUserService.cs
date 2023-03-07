using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.Services
{
    public interface IUserService
    {
        public Task<User?> GetUserAsync(int id);
        public Task<User?> GetUsersByNameAsync(string username);
        public Task<User?> GetUsersBySiteAsync(Site site);
        public Task<User?> GetUsersByServiceAsync(Service service);
        public Task<User?> GetUserByMailAsync(string email);
        public Task<IEnumerable<User?>> GetUsersAsync();
        public Task<User?> PostUserAsync(User model);
        public Task<User?> PutUserAsync(User model);
        public Task<bool> DeleteUserAsync(int id);


    }
}
