using System.Collections.Generic;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Services
{
    public interface IPOIRepository
    {
        IEnumerable<PointsOfInterestDto> GetPointsOfInterest(int id);
        PointsOfInterestDto GetPointOfInterest(int cityId, int pointOfInterestId);
        PointsOfInterestDto CreatePointOfInterest(int cityId, PointOfInterestCreationDto pointOfInterest);
        void UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestUpdateDto pointOfInterestUpdate);
        void DeletePointOfInterest(int cityId, int pointOfInterestId);
    }
}
