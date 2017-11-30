using FluentValidation;
using NetCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Services
{
    public class CityUpdateDTOValidator : AbstractValidator<CityUpdateDto>
    {
        public CityUpdateDTOValidator()
        {
            //TODO: Validation rules
        }
    }
}
