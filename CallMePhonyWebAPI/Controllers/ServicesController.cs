using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CallMePhonyWebAPI.Data;
using CallMePhonyWebAPI.Models;
using CallMePhonyWebAPI.Services;

namespace CallMePhonyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// Get a Service by it's id
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Services/1
        ///
        /// </remarks>
        /// <response code="200">Returns a Service model</response>
        /// <response code="404">If the item doesn't exist</response>
        /// <response code="400">If the id is 0</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetServiceAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("ID can't be 0");
                }

                Service? service = await _serviceService.GetServiceAsync(id);
                if (service != null)
                {
                    return StatusCode(StatusCodes.Status200OK, service);
                }
                return StatusCode(StatusCodes.Status404NotFound, $"Service with id: {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        /// <summary>
        /// Get all Services in a collection
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Services
        ///
        /// </remarks>
        /// <response code="200">Returns a Service model collection</response>
        /// <response code="204">If there is no Service</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetServicesAsync()
        {
            try
            {
                IEnumerable<Service?> services = await _serviceService.GetServicesAsync();
                if (services != null && services.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, services);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }

        }



        /// <summary>
        /// Create a new Service
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Services
        ///     
        /// </remarks>
        /// <response code="201">Returns the new Service created</response>
        /// <response code="400">If the Service model sent is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostServiceAsync(Service service)
        {
            try
            {
                if (service == null)
                {
                    return BadRequest("Service can't be null");
                }

                Service? newService = await _serviceService.PostServiceAsync(service);

                return StatusCode(StatusCodes.Status201Created, newService);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        /// <summary>
        /// Update a Service entity
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Services/1
        ///     
        /// </remarks>
        /// <response code="200">Returns the updated Service model</response>
        /// <response code="400">If id equals 0</response>
        /// <response code="404">If the Service doesn't exist</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUserAsync(int id, Service service)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("ID must not be 0");
                }

                if (!await _serviceService.ServiceExistsAsync(id))
                {
                    return NotFound($"Service with id: {id} not found");
                }

                Service? updatedService = await _serviceService.PutServiceAsync(service);
                return StatusCode(StatusCodes.Status200OK, updatedService);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }

        /// <summary>
        /// Delete a Service entity
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Services/1
        ///     
        /// </remarks>
        /// <response code="200">Returns a boolean isDeleted</response>
        /// <response code="404">If the Service doesn't exist</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteServiceAsync(int id)
        {
            try
            {
                if (!await _serviceService.ServiceExistsAsync(id))
                {
                    return NotFound($"Service with id: {id} not found");
                }

                bool isDeleted = await _serviceService.DeleteServiceAsync(id);
                return StatusCode(StatusCodes.Status200OK, isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
            }
        }
    }
}
