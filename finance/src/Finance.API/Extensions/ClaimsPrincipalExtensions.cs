using System.Security.Claims;

namespace Finance.API;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id == null) throw new InvalidOperationException("User Id not found in claims.");
        return Guid.Parse(id);
    }
}