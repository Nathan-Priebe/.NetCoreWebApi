using FluentValidation;
using NetCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Services
{
    public class CityCreationDTOValidator : AbstractValidator<CityCreationDto>
    {
        public CityCreationDTOValidator()
        {
            //TODO: Validation rules
        }
    }
}
