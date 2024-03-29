﻿using CallMePhonyEntities.Enums;
using CallMePhonyEntities.Models;
using CallMePhonyWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallMePhonyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SitesController : ControllerBase
    {
        private readonly ISiteService _siteService;

        public SitesController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        /// <summary>
        /// Get a Site by it's id
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Sites/1
        ///
        /// </remarks>
        /// <response code="200">Returns a Site model</response>
        /// <response code="404">If the item doesn't exist</response>
        /// <response code="400">If the id is 0</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetSiteAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("ID can't be 0");
                }

                Site? site = await _siteService.GetSiteAsync(id);
                if (site != null)
                {
                    return StatusCode(StatusCodes.Status200OK, site);
                }
                return StatusCode(StatusCodes.Status404NotFound, $"Site with id: {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        /// <summary>
        /// Get all Sites in a collection
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Sites
        ///
        /// </remarks>
        /// <response code="200">Returns a Site model collection</response>
        /// <response code="204">If there is no Site</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetSitesAsync()
        {
            try
            {
                IEnumerable<Site?> Sites = await _siteService.GetSitesAsync();
                if (Sites != null && Sites.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, Sites);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }

        }

        /// <summary>
        /// Create a new Site
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Sites
        ///     
        /// </remarks>
        /// <response code="201">Returns the new Site created</response>
        /// <response code="400">If the Site model sent is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [UserType(UserType = UserType.Admin)]
        public async Task<ActionResult> PostSiteAsync(Site site)
        {
            try
            {
                if (site == null)
                {
                    return BadRequest("Site can't be null");
                }

                Site? newSite = await _siteService.PostSiteAsync(site);

                return StatusCode(StatusCodes.Status201Created, newSite);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        /// <summary>
        /// Update a Site entity
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Sites/1
        ///     
        /// </remarks>
        /// <response code="200">Returns the updated Site model</response>
        /// <response code="400">If id equals 0</response>
        /// <response code="404">If the Site doesn't exist</response>
        [HttpPut("{id}")]
        [UserType(UserType = UserType.Admin)]
        public async Task<ActionResult> PutSiteAsync(int id, Site site)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("ID must not be 0");
                }

                if (!await _siteService.SiteExistsAsync(id))
                {
                    return NotFound($"Site with id: {id} not found");
                }

                Site? updatedSite = await _siteService.PutSiteAsync(site);
                return StatusCode(StatusCodes.Status200OK, updatedSite);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        /// <summary>
        /// Delete a Site entity
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Sites/1
        ///     
        /// </remarks>
        /// <response code="200">Returns a boolean isDeleted</response>
        /// <response code="404">If the Site doesn't exist</response>
        [HttpDelete("{id}")]
        [UserType(UserType = UserType.Admin)]
        public async Task<ActionResult> DeleteSiteAsync(int id)
        {
            try
            {
                if (!await _siteService.SiteExistsAsync(id))
                {
                    return NotFound($"Site with id: {id} not found");
                }

                bool isDeleted = await _siteService.DeleteSiteAsync(id);
                return StatusCode(StatusCodes.Status200OK, isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }
    }
}
