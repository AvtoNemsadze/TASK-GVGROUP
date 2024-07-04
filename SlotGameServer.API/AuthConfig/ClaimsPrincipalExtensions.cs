using SlotGameServer.Application.Constants;

namespace SlotGameServer.API.AuthConfig
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            var currentUserId = httpContextAccessor?.HttpContext?.User.FindFirst(CustomClaimTypes.Uid)?.Value;

            return int.TryParse(currentUserId, out var userId) ? userId : (int?)null;
        }
    }
}
