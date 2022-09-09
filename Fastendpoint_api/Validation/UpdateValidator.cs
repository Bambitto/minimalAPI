
using Fastendpoint_api.Models;

namespace Fastendpoint_api.Validation
{
    public class UpdateValidator : Validator<UpdateRequest>
    {

        private readonly string[] roles = { "admin", "default" };
        public UpdateValidator()
        {

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id cannot be empty");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .EmailAddress();

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("Role cannot be empty")
                .Must(r => roles.Contains(r))
                .WithMessage("Invalid role");

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
