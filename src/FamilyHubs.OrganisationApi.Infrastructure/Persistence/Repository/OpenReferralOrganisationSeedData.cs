using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.ServiceDirectory.Shared.Enums;

namespace FamilyHubs.Organisation.Infrastructure.Persistence.Repository;

public class OpenReferralOrganisationSeedData
{
    public static IReadOnlyCollection<OrganisationTypeEx> SeedOrgansisationType()
    {
        List<OrganisationTypeEx> organistionTypes = new()
        {
            new OrganisationTypeEx("1","LA","Local Authority"),
            new OrganisationTypeEx("2","VCFS","Voluntary, Charitable, Faith Sector")
        };

        return organistionTypes;
    }

    public static IReadOnlyCollection<UserTypeEx> SeedUserType()
    {
        List<UserTypeEx> organistionTypes = new()
        {
            new UserTypeEx("1","Organisation","An Organisation user"),
            new UserTypeEx("2","Public","A general public user")
        };

        return organistionTypes;
    }

    public static IReadOnlyCollection<RoleEx> SeedRole()
    {
        List<RoleEx> organistionTypes = new()
        {
            new RoleEx("1","SysAdmin","Administrator with system wide permissions"),
            new RoleEx("2","OrgAdmin","Administrator with permissions to  edit the organisation details and add, edit and delete services belonging to the users organisation"),
            new RoleEx("3","SvcAdmin","Administrator with permissions to  add, edit and delete services belonging to the users organisation"),
            new RoleEx("4","Pro","Professional that can create referrals and view/edit received referrals")
        };

        return organistionTypes;
    }
    public IReadOnlyCollection<OpenReferralOrganisationEx> SeedOpenReferralOrganistions()
    {
        List<OpenReferralOrganisationEx> openReferralOrganistions = new()
        {
            new OpenReferralOrganisationEx(
            "72e653e8-1d05-4821-84e9-9177571a6013",
            "Bristol County Council",
            "Bristol County Council",
            null,
            new Uri("https://www.bristol.gov.uk/").ToString(),
            "https://www.bristol.gov.uk/",
            string.Empty,
            "General Enquiries",
            "0300 123 6701",
            SeedOrgansisationType().FirstOrDefault()
            )
        };

        return openReferralOrganistions;
    }
}
