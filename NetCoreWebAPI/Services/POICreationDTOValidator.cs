using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Services
{
    public class PoiCreationDtoValidator : AbstractValidator<PointOfInterestCreationDto>
    {
        public PoiCreationDtoValidator()
        {
            RuleFor(poi => poi.Name).NotEmpty().NotNull().WithMessage("A point of interest name is mandatory");
            RuleFor(poi => poi.Name).Length(0, 50);
            RuleFor(poi => poi.Description).NotEmpty().NotNull().WithMessage("A point of interest must have a description");
            RuleFor(poi => poi.Description).Length(10, 200);
            RuleFor(poi => poi.Description).Must((x, Description) => !x.Name.Equals(Description, StringComparison.OrdinalIgnoreCase))
                .WithMessage("A point of interest cannot have the same name as the city");
        }
    }
}
