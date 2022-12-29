using Microsoft.AspNetCore.Identity;

namespace FluentPOS.Identity.API.Models;

public class ApplicationUser : IdentityUser<long>
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}