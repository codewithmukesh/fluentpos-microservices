using FluentPOS.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Identity.API.Data;

public sealed class IdentityContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long,
    IdentityUserClaim<long>,
    IdentityUserRole<long>, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    public IdentityContext(DbContextOptions<IdentityContext> options, IHttpContextAccessor httpContextAccessor) :
        base(options)
    {
    }
}
