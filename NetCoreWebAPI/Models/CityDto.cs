using System.Collections.Generic;

namespace NetCoreWebAPI.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public int NumberOfPointOfInterest { get; set; }
        public List<PointsOfInterestDto> PointsOfInterest { get; set; }

        public CityDto()
        {
            PointsOfInterest = new List<PointsOfInterestDto>();
            //NumberOfPointOfInterest = PointsOfInterest.Count();
        }
    }
}
