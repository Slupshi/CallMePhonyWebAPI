using CallMePhonyWebAPI.Models;

namespace CallMePhonyWebAPI.Services
{
    public interface ISiteService
    {
        /// <summary>
        /// Get a Site entity with its id
        /// </summary>
        /// <param name="id">Site's id</param>
        /// <returns>A Site entity</returns>
        public Task<Site?> GetSiteAsync(int id);
        /// <summary>
        /// Get all Sites entities with their Sitename
        /// </summary>
        /// <param name="name">Site's Sitename</param>
        /// <returns>A Site entities collection</returns>
        public Task<IEnumerable<Site?>> GetSitesByNameAsync(string name);
        /// <summary>
        /// Get all Site entities
        /// </summary>
        /// <returns>A Site entities collection</returns>
        public Task<IEnumerable<Site?>> GetSitesAsync();
        /// <summary>
        /// Create a Site entity in the database
        /// </summary>
        /// <param name="model">A Site model</param>
        /// <returns>The new Site entity</returns>
        public Task<Site?> PostSiteAsync(Site model);
        /// <summary>
        /// Update a Site entity in the database
        /// </summary>
        /// <param name="model">A Site model</param>
        /// <returns>The updated Site entity</returns>
        public Task<Site?> PutSiteAsync(Site model);
        /// <summary>
        /// Delete a Site entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean which show if the Site entity is deleted or not</returns>
        public Task<bool> DeleteSiteAsync(int id);

        /// <summary>
        /// Determine if the Site entity exists in the database with its id
        /// </summary>
        /// <param name="id">Site's id</param>
        /// <returns>A boolean</returns>
        public Task<bool> SiteExistsAsync(int id);
    }
}
