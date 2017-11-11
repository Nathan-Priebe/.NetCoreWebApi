using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with id {cityId} was not found");
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{cityId}/pointsofinterest/{pointofinterestId}", Name="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestCreation pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Name == pointOfInterest.Desc)
            {
                ModelState.AddModelError("Description", "Name and description should not be the same");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(x => x.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointsOfInterest()
            {
                Id = ++maxPointOfInterestId,
                Desc = pointOfInterest.Desc,
                Name = pointOfInterest.Name
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new {cityID = cityId, id = finalPointOfInterest.Id},
                finalPointOfInterest);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, [FromBody] PointOfInterestUpdate pointOfInterestUpdate)
        {
            if (pointOfInterestUpdate == null)
            {
                return BadRequest();
            }

            if (pointOfInterestUpdate.Name == pointOfInterestUpdate.Desc)
            {
                ModelState.AddModelError("Description", "Name and description should not be the same");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(x => x.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterestUpdate.Name;
            pointOfInterestFromStore.Desc = pointOfInterestUpdate.Desc;

            return NoContent();
        }

        [HttpPatch("{cityId}/pointofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
            [FromBody] JsonPatchDocument<PointOfInterestUpdate> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(x => x.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestUpdate()
            {
                Name = pointOfInterestFromStore.Name,
                Desc = pointOfInterestFromStore.Desc
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Name == pointOfInterestToPatch.Desc)
            {
                ModelState.AddModelError("Description", "Name and description should not be the same");
            }

            TryValidateModel(pointOfInterestToPatch);

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Desc = pointOfInterestToPatch.Desc;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(x => x.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            return NoContent();
        }
    }
}