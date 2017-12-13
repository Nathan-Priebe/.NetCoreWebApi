using FluentValidation;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Services
{
    public class POIUpdateDTOValidator : AbstractValidator<PointOfInterestUpdateDto>
    {
        public POIUpdateDTOValidator()
        {
            RuleFor(poi => poi.Name).NotEmpty().NotNull().WithMessage("Point of interest name is mandatory");
            RuleFor(poi => poi.Name).Length(5, 50);
            RuleFor(poi => poi.Description).NotEmpty().NotNull().WithMessage("Point of interest must have a description");
            RuleFor(poi => poi.Description).Length(10, 500);
        }
    }
}
