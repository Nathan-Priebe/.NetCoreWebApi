using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Services;
using AutoMapper;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private ICityInfoRepository _cityInfoRepository;
        private IPOIRepository _poiRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, ICityInfoRepository cityInfoRepository, IPOIRepository poiRepository)
        {
            _logger = logger;
            _cityInfoRepository = cityInfoRepository;
            _poiRepository = poiRepository;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                return Ok(_poiRepository.GetPointsOfInterest(cityId));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{cityId}/pointsofinterest/{pointofinterestId}", Name="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            return Ok(_poiRepository.GetPointOfInterest(cityId, pointOfInterestId));
        }

        [HttpPost("{cityId}/pointofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPointOfInterestToReturn = _poiRepository.CreatePointOfInterest(cityId, pointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
                { cityId = cityId, id = createdPointOfInterestToReturn.Id }, createdPointOfInterestToReturn);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, [FromBody] PointOfInterestUpdateDto pointOfInterestUpdate)
        {
            if (pointOfInterestUpdate == null)
            {
                return BadRequest();
            }

            if (pointOfInterestUpdate.Description == pointOfInterestUpdate.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _poiRepository.UpdatePointOfInterest(cityId, pointOfInterestId, pointOfInterestUpdate);

            return NoContent();
        }

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

        [HttpDelete("{cityId}/pointofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
           _poiRepository.DeletePointOfInterest(cityId, pointOfInterestId);

            return NoContent();
        }
    }
}