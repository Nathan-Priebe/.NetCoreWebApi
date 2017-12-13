using FluentValidation;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI.Services
{
    public class CityCreationDTOValidator : AbstractValidator<CityCreationDto>
    {
        public CityCreationDTOValidator()
        {
            RuleFor(city => city.Name).NotEmpty().NotNull().WithMessage("A point of interest name is mandatory");
            RuleFor(city => city.Name).Length(0, 50);
            RuleFor(city => city.Description).NotEmpty().NotNull().WithMessage("A point of interest must have a description");
            RuleFor(city => city.Description).Length(10, 200);
        }
    }
}
