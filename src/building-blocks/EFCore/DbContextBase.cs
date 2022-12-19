using BuildingBlocks.Auth;
using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.EFCore;
public abstract class DbContextBase : DbContext
{
    protected readonly ICurrentUserService _currentUser;
    public DbContextBase(DbContextOptions options, ICurrentUserService currentUser) : base(options)
    {
        _currentUser = currentUser;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        HandleAuditingBeforeSaveChanges(_currentUser.UserId);

        int result = await base.SaveChangesAsync(cancellationToken);

        //await SendDomainEventsAsync();

        return result;
    }

    private void HandleAuditingBeforeSaveChanges(string userId)
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.LastModifiedBy = userId;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = userId;
                    break;

                case EntityState.Deleted:
                    entry.Entity.DeletedBy = userId;
                    entry.Entity.DeletedOn = DateTime.UtcNow;
                    entry.State = EntityState.Modified;

                    break;
            }
        }
    }

}
