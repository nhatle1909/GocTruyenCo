using Application.DTO;
using FluentValidation;

namespace Controller.FluentValidation
{
    public class CreateAccountValidation : AbstractValidator<CreateAccountDTO>
    {
        public CreateAccountValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email is not allowed to be empty or null");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email address is not valid");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is not allowed to be empty or null");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must has at least 8 character");
        }

    }
    public class UpdateAccountValidation : AbstractValidator<UpdateAccountDTO>
    {
        public UpdateAccountValidation()
        {
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is not allowed to be empty or null");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must has at least 8 character");
        }
    }
}
