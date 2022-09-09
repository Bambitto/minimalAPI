using Fastendpoint_api.Contracts.Requests;


namespace Fastendpoint_api.Validation
{
    public class LoginValidator : Validator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username cannot be empty");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty");
        }
    }
}
