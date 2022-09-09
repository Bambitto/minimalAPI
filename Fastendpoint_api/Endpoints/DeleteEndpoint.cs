using Fastendpoint_api.Models;

namespace Fastendpoint_api.Endpoints
{
    public class DeleteEndpoint : Endpoint<GetDeleteUserRequest>
    {
        private readonly UserDbContext _context;
        public DeleteEndpoint(UserDbContext context) => _context = context;
        public override void Configure()
        {
            Verbs(Http.DELETE);
            Routes("delete");
            Roles("admin");
            Validator<GetDeleteUserValidator>();

        }

        public override async Task HandleAsync(GetDeleteUserRequest req, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

            if (user is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(ct);

            await SendOkAsync(ct);
        }
    }
}
