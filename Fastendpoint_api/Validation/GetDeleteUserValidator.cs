namespace Fastendpoint_api.Validation
{
    public class GetDeleteUserValidator : Validator<GetDeleteUserRequest>
    {
        public GetDeleteUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id cannot be empty");
        }
    }
}
