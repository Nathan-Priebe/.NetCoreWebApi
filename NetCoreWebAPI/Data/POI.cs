using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NetCoreWebAPI.Controllers;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Services;

namespace NetCoreWebAPI.Data
{
    public class POI : IPOIRepository
    {
        private ICityInfoRepository _cityInfoRepository;
        private ILogger<PointsOfInterestController> _logger;

        public POI(ILogger<PointsOfInterestController> logger, CityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _cityInfoRepository = cityInfoRepository;
        }

        public IEnumerable<PointsOfInterestDto> GetPointsOfInterest(int id)
        {
            if (!_cityInfoRepository.CityExists(id))
            {
                _logger.LogInformation($"City with id {id} wasn't found when accessing points of interest.");
                throw new Exception();
            }

            var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(id);
                
            return Mapper.Map<IEnumerable<PointsOfInterestDto>>(pointsOfInterestForCity);
        }

        public PointsOfInterestDto GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                throw new Exception();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (pointOfInterest == null)
            {
                throw new Exception();
            }

            return Mapper.Map<PointsOfInterestDto>(pointOfInterest);
        }

        public PointsOfInterestDto CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterest)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                throw new Exception();
            }

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                throw new Exception("A problem happened while handling your request.");
            }

            var createdPointOfInterestToReturn = Mapper.Map<PointsOfInterestDto>(finalPointOfInterest);

            return Mapper.Map<PointsOfInterestDto>(finalPointOfInterest);
        }

        public void UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestUpdateDto pointOfInterestUpdate)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
               throw new Exception();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                throw new Exception();
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
                throw new Exception();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                throw new Exception();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                throw new Exception("A problem happened while handling your request.");
            }
        }
    }
}
