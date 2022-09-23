using FamilyHubs.Organisation.Core.Entities;

namespace FamilyHubs.OrganisationApi.IntegrationTests.Persistence.Organisations;

public class EfRepositoryDelete : BaseEfRepositoryTestFixture
{
    [Fact]
    public async Task DeletesOrOganisationAfterAddingIt()
    {
        // Arrange
        OpenReferralOrganisationEx newOrganisation = GetTestCountyCouncilRecord();
        ArgumentNullException.ThrowIfNull(newOrganisation, nameof(OpenReferralOrganisationEx));

        var openReferralOrganisationId = newOrganisation.Id;
        var repository = GetRepository<OpenReferralOrganisationEx>();
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        await repository.AddAsync(newOrganisation);

        // Act
        await repository.DeleteAsync(newOrganisation);

        // Assert
        Assert.DoesNotContain(await repository.ListAsync(),
            newOrganisation => newOrganisation.Id == openReferralOrganisationId);
    }
}
