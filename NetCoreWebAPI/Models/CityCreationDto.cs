using System.ComponentModel.DataAnnotations;

namespace NetCoreWebAPI.Models
{
    public class CityCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
