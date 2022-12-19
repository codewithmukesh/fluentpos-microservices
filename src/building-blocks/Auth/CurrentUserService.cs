using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BuildingBlocks.Auth;
public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        Claims = httpContextAccessor.HttpContext?.User?.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
    }

    public string UserId { get; }
    public List<KeyValuePair<string, string>> Claims { get; set; }
}