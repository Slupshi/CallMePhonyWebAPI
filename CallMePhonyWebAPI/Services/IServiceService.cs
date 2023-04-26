using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.Services
{
    public interface IServiceService
    {
        /// <summary>
        /// Get a Service entity with its id
        /// </summary>
        /// <param name="id">Service's id</param>
        /// <returns>A Service entity</returns>
        public Task<Service?> GetServiceAsync(int id);
        /// <summary>
        /// Get a Service entity with its id as no tracking
        /// </summary>
        /// <param name="id">Service's id</param>
        /// <returns>A Service entity</returns>
        public Task<Service?> GetServiceAsNoTrackingAsync(int id);
        /// <summary>
        /// Get all Services entities with their Servicename
        /// </summary>
        /// <param name="name">Service's Servicename</param>
        /// <returns>A Service entities collection</returns>
        public Task<IEnumerable<Service?>> GetServicesByNameAsync(string name);
        /// <summary>
        /// Get all Service entities
        /// </summary>
        /// <returns>A Service entities collection</returns>
        public Task<IEnumerable<Service?>> GetServicesAsync();
        /// <summary>
        /// Create a Service entity in the database
        /// </summary>
        /// <param name="model">A Service model</param>
        /// <returns>The new Service entity</returns>
        public Task<Service?> PostServiceAsync(Service model);
        /// <summary>
        /// Update a Service entity in the database
        /// </summary>
        /// <param name="model">A Service model</param>
        /// <returns>The updated Service entity</returns>
        public Task<Service?> PutServiceAsync(Service model);
        /// <summary>
        /// Delete a Service entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean which show if the Service entity is deleted or not</returns>
        public Task<bool> DeleteServiceAsync(int id);

        /// <summary>
        /// Determine if the Service entity exists in the database with its id
        /// </summary>
        /// <param name="id">Service's id</param>
        /// <returns>A boolean</returns>
        public Task<bool> ServiceExistsAsync(int id);
    }
}
