global using Fastendpoint_api.Validation;
using Fastendpoint_api.Contracts.Requests;
using Fastendpoint_api.Contracts.Responses;
using Fastendpoint_api.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fastendpoint_api.Endpoints

{
    public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
    {
        private readonly UserDbContext _context;
        public LoginEndpoint(UserDbContext context) => _context = context;

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("login");
            Validator<LoginValidator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Username == req.Username && c.Password == req.Password, ct);
            if (user is null)
            {
                ThrowError("Supplied credentials are invalid");
                return;
            }
            if (user.Role is null)
            {
                ThrowError("Something went wrong");
                return;
            }

            if(user.Username is null)
            {
                ThrowError("");
            }

            var jwtToken = JWTBearer.CreateToken(
                signingKey: "secretkeysecretkeysecretkeysecretkey",
                expireAt: DateTime.UtcNow.AddDays(1),
                claims: new[] { ("Username", user.Username) },
                roles: new[] { user.Role.ToLower() });

            await SendAsync(new LoginResponse
            {
                Username = req.Username,
                Token = jwtToken
            }, cancellation: ct);
        }
    }
}
