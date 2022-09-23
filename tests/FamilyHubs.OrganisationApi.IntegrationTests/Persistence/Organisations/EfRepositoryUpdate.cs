using AutoFixture;
using FamilyHubs.Organisation.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OrganisationApi.IntegrationTests.Persistence.Organisations;

public class EfRepositoryUpdate : BaseEfRepositoryTestFixture
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task UpdatesOpenReferralOrganisationAfterAddingIt()
    {
        // Arrange
        OpenReferralOrganisationEx organisation = GetTestCountyCouncilRecord();
        ArgumentNullException.ThrowIfNull(organisation, nameof(OpenReferralOrganisationEx));
        

        var repository = GetRepository<OpenReferralOrganisationEx>();
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        await repository.AddAsync(organisation);

        DbContext.Entry(organisation).State = EntityState.Detached;             // detach the item so we get a different instance

        var addedOpenReferralOrganisation = await repository.GetByIdAsync(organisation.Id); // fetch the OpenReferralOrganisation and update its name
        if (addedOpenReferralOrganisation == null)
        {
            Assert.NotNull(addedOpenReferralOrganisation);
            return;
        }

        Assert.NotSame(organisation, addedOpenReferralOrganisation);

        // Act
        addedOpenReferralOrganisation.Name = "Brum Council";
        await repository.UpdateAsync(addedOpenReferralOrganisation);
        var updatedOpenReferralOrganisation = await repository.GetByIdAsync(addedOpenReferralOrganisation.Id);

        // Assert
        Assert.NotNull(updatedOpenReferralOrganisation);
        Assert.NotEqual(organisation.Name, updatedOpenReferralOrganisation?.Name);
        Assert.Equal(organisation.Id, updatedOpenReferralOrganisation?.Id);
    }
}
