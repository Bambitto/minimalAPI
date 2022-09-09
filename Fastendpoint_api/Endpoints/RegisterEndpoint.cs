using Fastendpoint_api.Models;

namespace Fastendpoint_api.Endpoints
{
    public class RegisterEndpoint : Endpoint<RegisterRequest>
    {

        private readonly UserDbContext _context;
        public RegisterEndpoint(UserDbContext context) => _context = context;
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("Register");
            AllowAnonymous();
            Validator<RegisterValidator>();
        }

        public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
        {
            if (await _context.Users.SingleOrDefaultAsync(x => x.Username == req.Username, cancellationToken: ct) is not null)
            {
                AddError("This username is taken");
            }

            if (await _context.Users.SingleOrDefaultAsync(x => x.Email == req.Email, cancellationToken: ct) is not null)
            {
                AddError("This email is taken");
            }
            ThrowIfAnyErrors();

            await _context.AddAsync(new User()
            {
                Username = req.Username,
                Email = req.Email,
                Password = req.Password,
                Role = "default",
                Id = Guid.NewGuid()
            }, ct);

            await _context.SaveChangesAsync(ct);

            await SendAsync("Registered successfully", cancellation: ct);

        }
    }
}
