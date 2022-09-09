using Fastendpoint_api.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fastendpoint_api.Endpoints
{
    public class UpdateEndpoint : Endpoint<UpdateRequest>
    {
        private readonly UserDbContext _context;


        public UpdateEndpoint(UserDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.PUT);
            Routes("update");
            Roles("admin");
            Validator<UpdateValidator>();
        }

        public override async Task HandleAsync(UpdateRequest req, CancellationToken ct)
        {
            var userForUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

            if (userForUpdate is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            if(await _context.Users.SingleOrDefaultAsync(x => x.Username == req.Username) is not null)
            {
                AddError("This username is taken");
            }

            if (await _context.Users.SingleOrDefaultAsync(x => x.Email == req.Email) is not null)
            {
                AddError("This email is taken");
            }

            ThrowIfAnyErrors();

            userForUpdate.Password = req.Password;
            userForUpdate.Role = req.Role;
            userForUpdate.Email = req.Email;
            userForUpdate.Username = req.Username;

            await _context.SaveChangesAsync(ct);

            await SendOkAsync(ct);
        }
    }
}
