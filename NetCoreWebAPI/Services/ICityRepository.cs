using System.Collections.Generic;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Services
{
    public interface ICityRepository
    {
        IEnumerable<CityWithoutPointsOfInterestDto> GetCities();
        CityDto GetCity(int id);
        CityWithoutPointsOfInterestDto GetCityWithoutPoi(int id);
    }
}
