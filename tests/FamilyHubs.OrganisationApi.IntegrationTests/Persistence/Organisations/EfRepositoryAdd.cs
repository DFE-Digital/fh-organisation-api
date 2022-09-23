using AutoFixture;
using FamilyHubs.Organisation.Core.Entities;

namespace FamilyHubs.OrganisationApi.IntegrationTests.Persistence.Organisations;

public class EfRepositoryAdd : BaseEfRepositoryTestFixture
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task AddsOrOpenReferralOrganisation()
    {
        // Arrange
        OpenReferralOrganisationEx organisation = GetTestCountyCouncilRecord();
        ArgumentNullException.ThrowIfNull(organisation, nameof(OpenReferralOrganisationEx));

        var repository = GetRepository<OpenReferralOrganisationEx>();
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        // Act
        await repository.AddAsync(organisation);

        var addedOpenReferralOrganisation = await repository.GetByIdAsync(organisation.Id);
        ArgumentNullException.ThrowIfNull(addedOpenReferralOrganisation, nameof(addedOpenReferralOrganisation));

        await repository.SaveChangesAsync();

        // Assert
        Assert.Equal(organisation, addedOpenReferralOrganisation);
        Assert.True(!string.IsNullOrEmpty(addedOpenReferralOrganisation.Id));
    }
}
