using Application.DTO;
using FluentValidation;

namespace Controller.FluentValidation
{
    public class AuthenticateDTOValidation : AbstractValidator<AuthenticateDTO>
    {
        public AuthenticateDTOValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email is not allowed to be empty or null");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email address is not valid");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is not allowed to be empty or null");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must has at least 8 character");
        }
    }
    public class SignUpDTOValidation : AbstractValidator<SignUpDTO>
    {
        public SignUpDTOValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email is not allowed to be empty or null");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email address is not valid");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is not allowed to be empty or null");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must has at least 8 character");
            RuleFor(x => x.Username).NotEmpty().NotNull().WithMessage("Name is not allowed to be empty or null");
        }
    }
}