namespace Fastendpoint_api.Contracts.Requests
{
    public class UpdateRequest
    {
        public Guid Id { get; init; }
        public string? Password { get; init; }
        public string? Username { get; init; }
        public string? Email { get; init; }
        public string? Role { get; init; }
    }
}
