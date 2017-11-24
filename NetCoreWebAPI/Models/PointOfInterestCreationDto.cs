using System.ComponentModel.DataAnnotations;

namespace NetCoreWebAPI.Models
{
    public class PointOfInterestCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
