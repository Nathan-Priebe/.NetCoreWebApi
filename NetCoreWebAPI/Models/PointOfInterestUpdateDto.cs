using System.ComponentModel.DataAnnotations;

namespace NetCoreWebAPI.Models
{
    public class PointOfInterestUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
