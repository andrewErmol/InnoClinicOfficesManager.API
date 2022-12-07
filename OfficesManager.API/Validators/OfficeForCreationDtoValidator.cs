using FluentValidation;
using OfficesManager.DTO.Office;

namespace OfficesManager.API.Validators
{
    public class OfficeForCreationDtoValidator : AbstractValidator<OfficeForCreationRequest>
    {
        public OfficeForCreationDtoValidator()
        {
            RuleFor(office => office.RegistryPhoneNumber).Matches(@"^(\+375)+\(\d\d\)\d\d\d-\d\d-\d\d");
            RuleFor(office => office.Address).NotNull().NotEmpty();
        }
    }
}
