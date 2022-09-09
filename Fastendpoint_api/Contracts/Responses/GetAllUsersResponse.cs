namespace Fastendpoint_api.Contracts.Responses
{
    public class GetAllUsersResponse
    {
        public IEnumerable<GetUserResponse>? Users { get; init; }
    }
}
