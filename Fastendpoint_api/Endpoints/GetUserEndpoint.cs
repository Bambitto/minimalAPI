using Fastendpoint_api.Models;

namespace Fastendpoint_api.Endpoints
{
    public class GetUserEndpoint : Endpoint<GetDeleteUserRequest, GetUserResponse>
    {

        private readonly UserDbContext _context;
        public GetUserEndpoint(UserDbContext context) => _context = context;

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("user");
            Roles("default", "admin");
            Validator<GetDeleteUserValidator>();
        }

        public override async Task HandleAsync(GetDeleteUserRequest req, CancellationToken ct)
        {
            var user = await _context.Users.FindAsync(new object?[] { req.Id }, cancellationToken: ct);

            if(user is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }
            var response = new GetUserResponse()
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };

            await SendOkAsync(response, ct);
        }
    }
}
