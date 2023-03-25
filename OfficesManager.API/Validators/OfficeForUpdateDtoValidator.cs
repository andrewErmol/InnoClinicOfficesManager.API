using FluentValidation;
using OfficesManager.DTO.Office;

namespace OfficesManager.API.Validators
{
    public class OfficeForUpdateDtoValidator : AbstractValidator<OfficeForCreationRequest>
    {
        public OfficeForUpdateDtoValidator()
        {
            RuleFor(office => office.RegistryPhoneNumber).Matches(@"^(\+375)+\(\d\d\)\d\d\d-\d\d-\d\d");
            RuleFor(office => office.Address).NotNull().NotEmpty();
        }
    }
}
