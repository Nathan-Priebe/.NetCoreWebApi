using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebAPI.Entities;

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
    }
}
