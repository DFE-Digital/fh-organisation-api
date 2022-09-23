using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Core.Interfaces;
using FamilyHubs.Organisation.Infrastructure.Persistence.Interceptors;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using FamilyHubs.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace FamilyHubs.OrganisationApi.IntegrationTests;

public abstract class BaseEfRepositoryTestFixture
{
    protected ApplicationDbContext DbContext; // see https://social.msdn.microsoft.com/Forums/en-US/930f159f-dfa5-4aa8-9af6-aa6545da5cbd/what-is-the-c-protected-property-naming-convention?forum=csharpgeneral

    protected BaseEfRepositoryTestFixture()
    {
        var options = CreateNewContextOptions();
        var mockEventDispatcher = new Mock<IDomainEventDispatcher>();
        var mockDateTime = new Mock<IDateTime>();
        var mockCurrentUserService = new Mock<ICurrentUserService>();
        var entitySaveChangesInterceptor = new EntitySaveChangesInterceptor(mockCurrentUserService.Object, mockDateTime.Object);


        DbContext = new ApplicationDbContext(options, mockEventDispatcher.Object, entitySaveChangesInterceptor);
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

    protected EfRepository<T> GetRepository<T>() where T : class, IAggregateRoot
    {
        return new EfRepository<T>(DbContext);
    }

    protected OpenReferralOrganisationEx GetTestCountyCouncilRecord()
    {
        return new OpenReferralOrganisationEx(
            "35c24999-9a92-44fa-8622-f1e35d39e8be",
            "Infra Test County Council",
            "Infra Test County Council",
            "logo",
            new Uri("https://www.test.gov.uk/").ToString(),
            "https://www.test.gov.uk/",
            "someone@aol.com",
            "Contact Name",
            "Contact Phone",
            new OrganisationTypeEx("0776464f-acc9-41c7-869a-b036704842fa", "TestType", "Is Test Type"));
    }

    protected OrganisationTypeEx GetOrganisationTypeRecord()
    {
        return new OrganisationTypeEx("04033b5b-deab-42d0-ae1d-2a5f9d6f7d63", "MyTestOrgType", "My Test Org Type");
    }
}
