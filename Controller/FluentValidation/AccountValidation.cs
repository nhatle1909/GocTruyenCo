using Application.DTO;
using FluentValidation;

namespace Controller.FluentValidation
{
    public class CommandAccountDTOValidation : AbstractValidator<CommandAccountDTO>
    {
        public CommandAccountDTOValidation()
        {
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is not allowed to be empty or null");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must has at least 8 character");
        }
    }
}
