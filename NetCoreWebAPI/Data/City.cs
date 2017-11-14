using System;
using System.Collections.Generic;
using AutoMapper;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Services;

namespace NetCoreWebAPI.Data
{
    public class City : ICityRepository
    {
        private ICityInfoRepository _cityInfoRepository;

        public City(ICityInfoRepository cityInfoRepository)
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
                throw new Exception();

            return Mapper.Map<CityWithoutPointsOfInterestDto>(city);
        }

        public CityDto GetCity(int id)
        {
            var city = _cityInfoRepository.GetCity(id, true);

            if (city == null)
                throw new Exception();

            return Mapper.Map<CityDto>(city);
        }
    }
}
