using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.ServiceDirectory.Shared.Enums;

namespace FamilyHubs.Organisation.Infrastructure.Persistence.Repository;

public class OpenReferralOrganisationSeedData
{
    public IReadOnlyCollection<OpenReferralOrganisation> SeedOpenReferralOrganistions()
    {
        List<OpenReferralOrganisation> openReferralOrganistions = new()
        {
            GetBristolCountyCouncil()
        };

        return openReferralOrganistions;
    }

    private OpenReferralOrganisation GetBristolCountyCouncil()
    {
        var bristolCountyCouncil = new OpenReferralOrganisation(
            "72e653e8-1d05-4821-84e9-9177571a6013",
            "Bristol County Council",
            "Bristol County Council",
            null,
            new Uri("https://www.bristol.gov.uk/").ToString(),
            "https://www.bristol.gov.uk/"
        );

        return bristolCountyCouncil;
    }
}
