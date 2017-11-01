using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int NumberOfPointOfInterest { get; set; }
        public List<PointsOfInterest> PointsOfInterest { get; set; }

        public City()
        {
            PointsOfInterest = new List<PointsOfInterest>();
            NumberOfPointOfInterest = PointsOfInterest.Count();
        }
    }
}
