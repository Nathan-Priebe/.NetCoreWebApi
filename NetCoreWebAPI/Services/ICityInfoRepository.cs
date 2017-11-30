using System.Collections.Generic;
using NetCoreWebAPI.Entities;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Services
{
    public interface ICityInfoRepository
    {
        bool CityExists(int cityId);
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointsOfInterest);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        bool Save();
        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        void DeleteCity(City city);
        void CreateCity(City city);
    }
}
