using System.Collections.Generic;
using AutoMapper;
using NetCoreWebAPI.Exceptions;
using NetCoreWebAPI.Models;

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
    }
}
