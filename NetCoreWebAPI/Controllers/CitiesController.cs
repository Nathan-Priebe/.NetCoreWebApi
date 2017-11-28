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

        [HttpPut("{cityId}/UpdateCity")]
        public IActionResult UpdateCity(int cityId, [FromBody] CityUpdateDto city)
        {
            //TODO: make call to repository to update city information
            return NoContent();
        }

        [HttpPost("{cityId}/CreateCity")]
        public IActionResult CreateCity(int id, [FromBody] CityCreationDto city)
        {
            //TODO: make call to repository to create city information
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCity(int id)
        {
            //TODO: make call to repository to remove city and all points of interest associated.
            return NoContent();
        }
    }
}