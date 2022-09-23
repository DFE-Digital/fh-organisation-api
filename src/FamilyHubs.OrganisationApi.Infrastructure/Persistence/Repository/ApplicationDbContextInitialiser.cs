using FamilyHubs.Organisation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Organisation.Infrastructure.Persistence.Repository;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync(IConfiguration configuration)
    {
        try
        {
            if (_context.Database.IsSqlServer() || _context.Database.IsNpgsql())
            {
                if (configuration.GetValue<bool>("RecreateDbOnStartup"))
                {
                    _context.Database.EnsureDeleted();
                    _context.Database.EnsureCreated();
                }
                else
                    await _context.Database.MigrateAsync();
            }
            //else
            //{
            //    _context.Database.EnsureDeleted();
            //}
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (_context.OpenReferralOrganisations.Any())
            return;

        var openReferralOrganisationSeedData = new OpenReferralOrganisationSeedData();

        _context.Roles.AddRange(OpenReferralOrganisationSeedData.SeedRole());
        _context.UserTypes.AddRange(OpenReferralOrganisationSeedData.SeedUserType());
        _context.OrganisationTypes.AddRange(OpenReferralOrganisationSeedData.SeedOrgansisationType());
        await _context.SaveChangesAsync();

        IReadOnlyCollection<OpenReferralOrganisationEx> openReferralOrganisations = openReferralOrganisationSeedData.SeedOpenReferralOrganistions();

        foreach (var openReferralOrganisation in openReferralOrganisations)
        {
            _context.OpenReferralOrganisations.Add(openReferralOrganisation);
        }

        await _context.SaveChangesAsync();

    }
}
