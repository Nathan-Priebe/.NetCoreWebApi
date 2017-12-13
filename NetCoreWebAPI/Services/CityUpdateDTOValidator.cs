using FluentValidation;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Services
{
    public class CityUpdateDTOValidator : AbstractValidator<CityUpdateDto>
    {
        public CityUpdateDTOValidator()
        {
            RuleFor(city => city.Name).NotEmpty().NotNull().WithMessage("Point of interest name is mandatory");
            RuleFor(city => city.Name).Length(5, 50);
            RuleFor(city => city.Description).NotEmpty().NotNull().WithMessage("Point of interest must have a description");
            RuleFor(city => city.Description).Length(10, 500);
        }
    }
}
