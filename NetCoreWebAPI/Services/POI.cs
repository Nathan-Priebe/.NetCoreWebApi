using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NetCoreWebAPI.Controllers;
using NetCoreWebAPI.Exceptions;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Services
{
    public class Poi : IPOIRepository
    {
        private ICityInfoRepository _cityInfoRepository;
        private ILogger<PointsOfInterestController> _logger;

        public Poi(ILogger<PointsOfInterestController> logger, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _cityInfoRepository = cityInfoRepository;
        }

        public IEnumerable<PointsOfInterestDto> GetPointsOfInterest(int id)
        {
            if (!_cityInfoRepository.CityExists(id))
            {
                _logger.LogInformation($"City with ID {id} wasn't found when accessing points of interest.");
                throw new NotFoundException($"City with the ID {id} wasn't found when accessing the points of interest");
            }

            var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(id);
                
            return Mapper.Map<IEnumerable<PointsOfInterestDto>>(pointsOfInterestForCity);
        }

        public PointsOfInterestDto GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                throw new NotFoundException($"City with the ID {cityId} wasn't found when accessing the point of interest");
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (pointOfInterest == null)
            {
                throw new NotFoundException($"Point of interest with the ID {pointOfInterestId} wasn't found");
            }

            return Mapper.Map<PointsOfInterestDto>(pointOfInterest);
        }

        public PointsOfInterestDto CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterest)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                throw new NotFoundException($"City with the ID of {cityId} wasn't found ");
            }

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                throw new Exception("A problem happened while handling your request.");
            }

            return Mapper.Map<PointsOfInterestDto>(finalPointOfInterest);
        }

        public void UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestUpdateDto pointOfInterestUpdate)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
               throw new NotFoundException($"City with the ID {cityId} wasn't found");
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                throw new NotFoundException($"City with the ID {cityId} wasn't found");
            }

            Mapper.Map(pointOfInterestUpdate, pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                throw new Exception("A problem happened while handling your request.");
            }
        }

        public void DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                throw new NotFoundException($"City with the ID {cityId} wasn't found");
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                throw new NotFoundException($"Point of interest with the ID {pointOfInterestId} wasn't found");
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                throw new Exception("A problem happened while handling your request.");
            }
        }
    }
}
