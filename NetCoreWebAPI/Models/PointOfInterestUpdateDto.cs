using System.ComponentModel.DataAnnotations;

namespace NetCoreWebAPI.Models
{
    public class PointOfInterestUpdateDto
    {
        [Required(ErrorMessage = "A name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
