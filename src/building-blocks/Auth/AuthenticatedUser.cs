using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BuildingBlocks.Auth;
public class AuthenticatedUser : IAuthenticatedUser
{
    public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user is not null)
        {
            Id = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            Claims = user.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
            Email = user.FindFirstValue(ClaimTypes.Email);
        }
    }

    public string? Id { get; }
    public string? Email { get; }
    public List<KeyValuePair<string, string>>? Claims { get; set; }
}