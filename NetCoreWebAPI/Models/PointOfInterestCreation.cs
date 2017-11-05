using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Models
{
    public class PointOfInterestCreation
    {
        [Required(ErrorMessage = "A name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Desc { get; set; }
    }
}
