using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Services;
using NetCoreWebAPI.Exceptions;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public CitiesController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            throw new InternalErrorException();
            return Ok(_cityRepository.GetCities());
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            return includePointsOfInterest ? Ok(_cityRepository.GetCity(id)) : Ok(_cityRepository.GetCityWithoutPoi(id));
        }
    }
}