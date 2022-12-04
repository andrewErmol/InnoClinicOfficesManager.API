using FluentValidation;
using OfficesManager.DTO.Office;

namespace OfficesManager.API.Validators
{
    public class OfficeForCreationDtoValidator : AbstractValidator<OfficeForCreationDto>
    {
        public OfficeForCreationDtoValidator()
        {
            RuleFor(office => office.RegistryPhoneNumber).Matches(@"^(\+375)+\(\d\d\)\d\d\d-\d\d-\d\d");
            RuleFor(office => office.Address).NotNull().NotEmpty();
        }
    }
}
