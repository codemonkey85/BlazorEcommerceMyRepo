namespace BlazorECommerce.Server;

public static class SharedMethods
{
    public static (bool UserIdFound, int UserId) GetUserIdFromClaimsPrincipal(ClaimsPrincipal user) =>
        (int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId), userId);
}
