using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Services;
using NetCoreWebAPI.Exceptions;

namespace NetCoreWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public CitiesController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
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
    }
}