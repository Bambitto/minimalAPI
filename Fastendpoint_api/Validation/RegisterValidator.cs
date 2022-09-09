namespace Fastendpoint_api.Validation
{
    public class RegisterValidator : Validator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .MinimumLength(8)
                .WithMessage("Password has to contain at least 8 characters");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username cannot be empty")
                .MinimumLength(5)
                .WithMessage("Username has to contain at least 5 characters");

        }
    }
}
