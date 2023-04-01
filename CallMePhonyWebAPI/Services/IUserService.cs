using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Get a User entity with its id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>A User entity</returns>
        public Task<User?> GetUserAsync(int id);
        /// <summary>
        /// Get all Users entities with their username
        /// </summary>
        /// <param name="username">User's username</param>
        /// <returns>A User entities collection</returns>
        public Task<IEnumerable<User?>> GetUsersByNameAsync(string username);
        /// <summary>
        /// Get all Users entities with their site
        /// </summary>
        /// <param name="site">User's site</param>
        /// <returns>A User entities collection</returns>
        public Task<IEnumerable<User?>> GetUsersBySiteAsync(Site site);
        /// <summary>
        /// Get all Users entities with their service
        /// </summary>
        /// <param name="service">User's service</param>
        /// <returns>A User entities collection</returns>
        public Task<IEnumerable<User?>> GetUsersByServiceAsync(Service service);
        /// <summary>
        /// Get a User entity with its email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>A User entity</returns>
        public Task<User?> GetUserByMailAsync(string email);
        /// <summary>
        /// Get all User entities
        /// </summary>
        /// <returns>A User entities collection</returns>
        public Task<IEnumerable<User?>> GetUsersAsync();
        /// <summary>
        /// Create a User entity in the database
        /// </summary>
        /// <param name="model">A User model</param>
        /// <returns>The new User entity</returns>
        public Task<User?> PostUserAsync(User model);
        /// <summary>
        /// Update a User entity in the database
        /// </summary>
        /// <param name="model">A User model</param>
        /// <returns>The updated User entity</returns>
        public Task<User?> PutUserAsync(User model);
        /// <summary>
        /// Delete a User entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean which show if the User entity is deleted or not</returns>
        public Task<bool> DeleteUserAsync(int id);

        /// <summary>
        /// Determine if the User entity exists in the database with its id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>A boolean</returns>
        public Task<bool> UserExistsAsync(int id);
    }
}
