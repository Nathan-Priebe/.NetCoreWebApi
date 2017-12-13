using System.ComponentModel.DataAnnotations;

namespace NetCoreWebAPI.Models
{
    public class CityUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
