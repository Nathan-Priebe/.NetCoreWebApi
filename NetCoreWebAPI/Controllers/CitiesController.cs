using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Services;
using NetCoreWebAPI.Exceptions;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityRepository cityRepository, ICityInfoRepository cityInfoRepository)
        {
            _cityRepository = cityRepository;
            _cityInfoRepository = cityInfoRepository;
        }

        /// <summary>
        /// Returns a List of cities
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(_cityRepository.GetCities());
        }

        /// <summary>
        /// Returns a list of cities with/without points of interest
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includePointsOfInterest">if set to <c>true</c> [does include points of interest].</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            return includePointsOfInterest ? Ok(_cityRepository.GetCity(id)) : Ok(_cityRepository.GetCityWithoutPoi(id));
        }

        /// <summary>
        /// Updates the city.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        [HttpPut("{cityId}/UpdateCity")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        public IActionResult UpdateCity(int cityId, [FromBody] CityUpdateDto city)
        {
            _cityRepository.UpdateCity(cityId, city);
            return NoContent();
        }

        /// <summary>
        /// Creates the city.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        [HttpPost("{cityId}/CreateCity")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateCity(int id, [FromBody] CityCreationDto city)
        {
            _cityRepository.CreateCity(city);
            return Ok();
        }

        /// <summary>
        /// Deletes the city.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{cityId}/Delete")]
        [ProducesResponseType(204)]
        public IActionResult DeleteCity(int id)
        {
            _cityRepository.DeleteCity(id);
            return NoContent();
        }
    }
}