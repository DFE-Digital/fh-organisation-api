using FamilyHubs.Organisation.Core.Dto;
using FamilyHubs.Organisation.Core.Interfaces;
using FamilyHubs.Organisation.Infrastructure.Persistence.Interceptors;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using FamilyHubs.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace FamilyHubs.OrganisationApi.UnitTests;

public class BaseCreateDbUnitTest
{
    protected ApplicationDbContext GetApplicationDbContext()
    {
        var options = CreateNewContextOptions();
        var mockEventDispatcher = new Mock<IDomainEventDispatcher>();
        var mockDateTime = new Mock<IDateTime>();
        var mockCurrentUserService = new Mock<ICurrentUserService>();
        var auditableEntitySaveChangesInterceptor = new EntitySaveChangesInterceptor(mockCurrentUserService.Object, mockDateTime.Object);
        var mockApplicationDbContext = new ApplicationDbContext(options, mockEventDispatcher.Object, auditableEntitySaveChangesInterceptor);

        return mockApplicationDbContext;
    }

    protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
    {
        // Create a fresh service provider, and therefore a fresh
        // InMemory database instance.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance telling the context to use an
        // InMemory database and the new service provider.
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseInMemoryDatabase("AuthicationDb")
               .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }

    protected OpenReferralOrganisationExDto GetTestCountyCouncilRecord()
    {
        return new OpenReferralOrganisationExDto(
            "f16ab625-51e8-4383-a680-f8648cff0620",
            "1",
            "Unit Test County Council",
            "Unit Test County Council",
            "logo",
            new Uri("https://www.test.gov.uk/").ToString(),
            "https://www.test.gov.uk/",
            "someone@aol.com",
            "Contact Name",
            "Contact Phone");
    }
}
