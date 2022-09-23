using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Core.Interfaces.Infrastructure.Persistence;
using FamilyHubs.Organisation.Infrastructure.Persistence.Interceptors;
using FamilyHubs.SharedKernel;
using FamilyHubs.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FamilyHubs.Organisation.Infrastructure.Persistence.Repository
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly EntitySaveChangesInterceptor _entitySaveChangesInterceptor;

        public ApplicationDbContext
        (
            DbContextOptions<ApplicationDbContext> options,
            IDomainEventDispatcher dispatcher,
            EntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
        )
        : base(options)
        {
            _dispatcher = dispatcher;
            _entitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.AddInterceptors(_entitySaveChangesInterceptor);
        }

        public DbSet<OpenReferralOrganisationEx> OpenReferralOrganisations => Set<OpenReferralOrganisationEx>();
        public DbSet<OrganisationTypeEx> OrganisationTypes => Set<OrganisationTypeEx>();
        public DbSet<RoleEx> Roles => Set<RoleEx>();
        public DbSet<UserTypeEx> UserTypes => Set<UserTypeEx>();
        public DbSet<UserEx> Users => Set<UserEx>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<EntityBase<string>>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
