using System.ComponentModel.DataAnnotations;

namespace NetCoreWebAPI.Models
{
    public class PointOfInterestUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
