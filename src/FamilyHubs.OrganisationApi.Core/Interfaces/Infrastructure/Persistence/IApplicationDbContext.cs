using FamilyHubs.Organisation.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Organisation.Core.Interfaces.Infrastructure.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<OpenReferralOrganisation> OpenReferralOrganisations { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}