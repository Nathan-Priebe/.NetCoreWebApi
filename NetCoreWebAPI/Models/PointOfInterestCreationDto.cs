using System.ComponentModel.DataAnnotations;

namespace NetCoreWebAPI.Models
{
    public class PointOfInterestCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
