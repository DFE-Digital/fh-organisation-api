using FamilyHubs.Organisation.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Organisation.Core.Interfaces.Infrastructure.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<OpenReferralOrganisationEx> OpenReferralOrganisations { get; }
        DbSet<OrganisationTypeEx> OrganisationTypes { get; }
        DbSet<RoleEx> Roles { get; }
        DbSet<UserTypeEx> UserTypes { get; }
        DbSet<UserEx> Users { get; }
        DbSet<UserOrganisationEx> UserOrganisations { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}