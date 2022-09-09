using Fastendpoint_api.Contracts.Responses;
using Fastendpoint_api.Models;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fastendpoint_api.Endpoints
{
    public class GetAllUsersEndpoint : EndpointWithoutRequest<GetAllUsersResponse>
    {
        private readonly UserDbContext _context;
        public GetAllUsersEndpoint(UserDbContext context) => _context = context;

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("users");
            Roles("default", "admin");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var users = await _context.Users.ToListAsync(ct);
            if(users is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var response = new GetAllUsersResponse()
            {
                Users = users.Select(x => new GetUserResponse
                {
                    Username = x.Username,
                    Email = x.Email,
                    Role = x.Role
                })
            };

            await SendAsync(response, cancellation: ct);
        }
    }
}
