using System.Collections.Generic;
using AutoMapper;
using NetCoreWebAPI.Exceptions;
using NetCoreWebAPI.Models;
using System;

namespace NetCoreWebAPI.Services
{
    public class Cities : ICityRepository
    {
        private ICityInfoRepository _cityInfoRepository;

        public Cities(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }
        public IEnumerable<CityWithoutPointsOfInterestDto> GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            return Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);
        }

        public CityWithoutPointsOfInterestDto GetCityWithoutPoi(int id)
        {
            var city = _cityInfoRepository.GetCity(id, false);

            if (city == null)
                throw new NotFoundException($"City with the ID of {id} could not be found");

            return Mapper.Map<CityWithoutPointsOfInterestDto>(city);
        }

        public CityDto GetCity(int id)
        {
            var city = _cityInfoRepository.GetCity(id, true);

            if (city == null)
                throw new NotFoundException($"City with the ID of {id} could not be found");

            return Mapper.Map<CityDto>(city);
        }

        public CityDto CreateCity(CityCreationDto city)
        {
            var finalCity = Mapper.Map<Entities.City>(city);

            _cityInfoRepository.CreateCity(finalCity);

            if (!_cityInfoRepository.Save())
            {
                throw new Exception("A problem happened while handling your request.");
            }

            return Mapper.Map<CityDto>(finalCity);
        }

        public void UpdateCity(int cityId, CityUpdateDto city)
        {
            var cityEntity = _cityInfoRepository.GetCity(cityId, false);
            if (cityEntity == null)
            {
                throw new NotFoundException($"City with the ID {cityId} wasn't found");
            }

            Mapper.Map(city, cityEntity);

            if (!_cityInfoRepository.Save())
            {
                throw new Exception("A problem happened while handling your request.");
            }
        }

        public void DeleteCity(int cityId)
        {
            var city = _cityInfoRepository.GetCity(cityId, false);
            _cityInfoRepository.DeleteCity(city);

        }
    }
}
