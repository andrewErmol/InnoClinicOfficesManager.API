using FluentValidation;
using OfficesManager.DTO;

namespace OfficesManager.API.Validators
{
    public class ArgumentsForPaginationValidator : AbstractValidator<ArgumentsForPagination>
    {
        public ArgumentsForPaginationValidator()
        {
            RuleFor(arg => arg.offset).GreaterThan(-1).LessThan(arg => arg.limit);
        }
    }
}
