using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Services;
using AutoMapper;

namespace NetCoreWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        private IPOIRepository _poiRepository;

        public PointsOfInterestController(ICityInfoRepository cityInfoRepository, IPOIRepository poiRepository)
        {
            _cityInfoRepository = cityInfoRepository;
            _poiRepository = poiRepository;
        }

        /// <summary>
        /// Returns a list of points of interest for a given city Id
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        { 
                return Ok(_poiRepository.GetPointsOfInterest(cityId));
        }

        /// <summary>
        /// Returns a specific point of interest for a specific city Id
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="pointOfInterestId">The point of interest identifier.</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{cityId}/pointsofinterest/{pointofinterestId}", Name="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            return Ok(_poiRepository.GetPointOfInterest(cityId, pointOfInterestId));
        }

        /// <summary>
        /// Creates the point of interest for a specific city ID
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="pointOfInterest">The point of interest.</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPost("{cityId}/pointofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestCreationDto pointOfInterest)
        {
            _poiRepository.CreatePointOfInterest(cityId, pointOfInterest);

            return Ok();
        }

        /// <summary>
        /// Updates a specific point of interest in a specified city 
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="pointOfInterestId">The point of interest identifier.</param>
        /// <param name="pointOfInterestUpdate">The point of interest update.</param>
        /// <returns></returns>
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, [FromBody] PointOfInterestUpdateDto pointOfInterestUpdate)
        {
            _poiRepository.UpdatePointOfInterest(cityId, pointOfInterestId, pointOfInterestUpdate);

            return NoContent();
        }

        /// <summary>
        /// Partially updates a point of interest
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="pointOfInterestId">The point of interest identifier.</param>
        /// <param name="patchDoc">The patch document.</param>
        /// <returns></returns>
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        [HttpPatch("{cityId}/pointofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
            [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, cityId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = Mapper.Map<PointOfInterestUpdateDto>(pointOfInterestEntity);

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes the point of interest.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="pointOfInterestId">The point of interest identifier.</param>
        /// <returns></returns>
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        [HttpDelete("{cityId}/pointofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
           _poiRepository.DeletePointOfInterest(cityId, pointOfInterestId);

            return NoContent();
        }
    }
}