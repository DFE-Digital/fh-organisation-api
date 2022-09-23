using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.EntityFrameworkCore;

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
    public IReadOnlyCollection<OpenReferralOrganisationEx> SeedOpenReferralOrganistions(List<OrganisationTypeEx> orgTypes)
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
            "0117 922 2000",
            orgTypes.FirstOrDefault(x => x.Id == "1")
            ),

            new OpenReferralOrganisationEx(
            "3c114eb8-f904-4f7b-a852-b4ccfc2982a6",
            "Lancashire County Council",
            "Lancashire County Council",
            null,
            new Uri("https://www.lancashire.gov.uk/").ToString(),
            "https://www.lancashire.gov.uk/",
            "enquiries@lancashire.gov.uk",
            "General Enquiries",
            "0300 123 6701",
            orgTypes.FirstOrDefault(x => x.Id == "1")
            ),

            new OpenReferralOrganisationEx(
            "50334e7b-74d4-489f-a13a-9e43cf9a61ca",
            "London Borough of Redbridge",
            "London Borough of Redbridge",
            null,
            new Uri("https://www.redbridge.gov.uk/").ToString(),
            "https://www.redbridge.gov.uk/",
            string.Empty,
            "Customer Service",
            "020 8554 5000",
            orgTypes.FirstOrDefault(x => x.Id == "1")
            ),

            new OpenReferralOrganisationEx(
            "f117a22e-c1b1-4b66-890e-9382b9582b7e",
            "Salford City Council",
            "Salford City Council",
            null,
            new Uri("https://www.salford.gov.uk/").ToString(),
            "https://www.salford.gov.uk/",
            string.Empty,
            "General Enquiries",
            "0161 793 3303",
            orgTypes.FirstOrDefault(x => x.Id == "1")
            ),

            new OpenReferralOrganisationEx(
            "a6311136-2db0-42db-aa49-4e982aea96f6",
            "Suffolk County Council",
            "Suffolk County Council",
            null,
            new Uri("https://www.suffolk.gov.uk/").ToString(),
            "https://www.suffolk.gov.uk/",
            "customer.services@​​suffolk.gov.uk",
            "General Enquiries",
            "0161 793 3303",
            orgTypes.FirstOrDefault(x => x.Id == "1")
            ),

            new OpenReferralOrganisationEx(
            "1817efcc-b8cf-494e-8a9d-450d67bf1674",
            "Tower Hamlets",
            "Tower Hamlets",
            null,
            new Uri("https://www.towerhamlets.gov.uk/").ToString(),
            "https://www.towerhamlets.gov.uk/",
            string.Empty,
            "General Enquiries",
            "020 7364 5000",
            orgTypes.FirstOrDefault(x => x.Id == "1")
            ),

            new OpenReferralOrganisationEx(
            "d9da4ce2-69b1-41ea-a535-fcda65dec44e",
            "Citizens Advice Bureau",
            "Citizens Advice Bureau",
            null,
            new Uri("https://www.citizensadvice.org.uk/").ToString(),
            "https://www.citizensadvice.org.uk/",
            string.Empty,
            "Advice Line",
            "0800 144 8848",
            orgTypes.FirstOrDefault(x => x.Id == "2")
            )
        };

        return openReferralOrganistions;
    }

    public IReadOnlyCollection<UserEx> SeedUsers(List<UserTypeEx> userTypes)
    {
        List<UserEx> users = new()
        {
            new UserEx(
                "BtlOrgAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "BtlOrgAdmin",
                "The Bristol City Council Organisation Administrator person",
                null,
                null,
                null),

            new UserEx(
                "BtlSvcAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "BtlSvcAdmin",
                "The Bristol City Council Service Administrator person",
                null,
                null,
                null),

            new UserEx(
                "BtlPro",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "BtlPro",
                "The BristolCityCouncil Organisation Professional person",
                null,
                null,
                null),

            new UserEx(
                "LanOrgAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "LanOrgAdmin",
                "The Lancashire Organisation Administrator person",
                null,
                null,
                null),

            new UserEx(
                "LanOrgSvcAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "LanOrgSvcAdmin",
                "The Lancashire Service Administrator person",
                null,
                null,
                null),

            new UserEx(
                "LanPro",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "LanPro",
                "The Lancashire Professional person",
                null,
                null,
                null),

            new UserEx(
                "LbrOrgAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "LbrOrgAdmin",
                "The London Borough of Redbridge Organisation Administrator person",
                null,
                null,
                null),

            new UserEx(
                "LbrSvcAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "LbrSvcAdmin",
                "The London Borough of Redbridge Service Administrator person",
                null,
                null,
                null),

            new UserEx(
                "LbrPro",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "LbrPro",
                "The London Borough of Redbridge Professional person",
                null,
                null,
                null),

            new UserEx(
                "SalOrgAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "SalOrgAdmin",
                "The Salford City Council Administrator person",
                null,
                null,
                null),

            new UserEx(
                "SalSvcAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "SalSvcAdmin",
                "The Salford City Council Service Administrator person",
                null,
                null,
                null),

            new UserEx(
                "SalPro",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "SalPro",
                "The Salford City Council Professional person",
                null,
                null,
                null),

            new UserEx(
                "SufOrgAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "SufOrgAdmin",
                "The Suffolk County Council Organisation Administrator person",
                null,
                null,
                null),

            new UserEx(
                "SufSvcAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "SufSvcAdmin",
                "The Suffolk County Council Service Administrator person",
                null,
                null,
                null),

            new UserEx(
                "SufPro",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "SufPro",
                "The Suffolk County Council Professional person",
                null,
                null,
                null),

            new UserEx(
                "TowOrgAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "TowOrgAdmin",
                "The Tower Hamlets Organisation Administrator person",
                null,
                null,
                null),

            new UserEx(
                "TowSvcAdmin",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "TowSvcAdmin",
                "The Tower Hamlets Organisation Service Administrator person",
                null,
                null,
                null),

            new UserEx(
                "TowPro",
                userTypes.FirstOrDefault(x => x.Id == "1"),
                "TowPro",
                "Tower Hamlets Organisation Professional person",
                null,
                null,
                null),

        };

        return users;
    }
    
    public IReadOnlyCollection<UserOrganisationEx> SeedUserOrganisations(List<OpenReferralOrganisationEx> orgs)
    {
        List<UserOrganisationEx> userOrganisations = new()
        {
            new UserOrganisationEx(
                "BtlOrgAdmin",
                orgs.FirstOrDefault(x => x.Name == "Bristol County Council").Id,
                "The Bristol City Council Organisation Administrator person"
                ),

            new UserOrganisationEx(
                "BtlSvcAdmin",
                orgs.FirstOrDefault(x => x.Name == "Bristol County Council").Id,
                "The Bristol City Council Service Administrator person"
                ),

            new UserOrganisationEx(
                "BtlPro",
                orgs.FirstOrDefault(x => x.Name == "Bristol County Council").Id,
                "The BristolCityCouncil Organisation Professional person"
                ),

            new UserOrganisationEx(
                "LanOrgAdmin",
                orgs.FirstOrDefault(x => x.Name == "Lancashire County Council").Id,
                "The Lancashire Organisation Administrator person"
                ),

            new UserOrganisationEx(
                "LanOrgSvcAdmin",
                orgs.FirstOrDefault(x => x.Name == "Lancashire County Council").Id,
                "The Lancashire Service Administrator person"),

            new UserOrganisationEx(
                "LanPro",
                orgs.FirstOrDefault(x => x.Name == "Lancashire County Council").Id,
                "The Lancashire Professional person"),

            new UserOrganisationEx(
                "LbrOrgAdmin",
                orgs.FirstOrDefault(x => x.Name == "London Borough of Redbridge").Id,
                "The London Borough of Redbridge Organisation Administrator person"),

            new UserOrganisationEx(
                "LbrSvcAdmin",
                orgs.FirstOrDefault(x => x.Name == "London Borough of Redbridge").Id,
                "The London Borough of Redbridge Service Administrator person"),

            new UserOrganisationEx(
                "LbrPro",
                orgs.FirstOrDefault(x => x.Name == "London Borough of Redbridge").Id,
                "The London Borough of Redbridge Professional person"),

            new UserOrganisationEx(
                "SalOrgAdmin",
                orgs.FirstOrDefault(x => x.Name == "Salford City Council").Id,
                "The Salford City Council Administrator person"),

            new UserOrganisationEx(
                "SalSvcAdmin",
                orgs.FirstOrDefault(x => x.Name == "Salford City Council").Id,
                "The Salford City Council Service Administrator person"),

            new UserOrganisationEx(
                "SalPro",
                orgs.FirstOrDefault(x => x.Name == "Salford City Council").Id,
                "The Salford City Council Professional person"),

            new UserOrganisationEx(
                "SufOrgAdmin",
                orgs.FirstOrDefault(x => x.Name == "Suffolk County Council").Id,
                "The Suffolk County Council Organisation Administrator person"),

            new UserOrganisationEx(
                "SufSvcAdmin",
                orgs.FirstOrDefault(x => x.Name == "Suffolk County Council").Id,
                "The Suffolk County Council Service Administrator person"),

            new UserOrganisationEx(
                "SufPro",
                orgs.FirstOrDefault(x => x.Name == "Suffolk County Council").Id,
                "The Suffolk County Council Professional person"),

            new UserOrganisationEx(
                "TowOrgAdmin",
                orgs.FirstOrDefault(x => x.Name == "Tower Hamlets").Id,
                "The Tower Hamlets Organisation Administrator person"),

            new UserOrganisationEx(
                "TowSvcAdmin",
                orgs.FirstOrDefault(x => x.Name == "Tower Hamlets").Id,
                "The Tower Hamlets Organisation Service Administrator person"),

            new UserOrganisationEx(
                "TowPro",
                orgs.FirstOrDefault(x => x.Name == "Tower Hamlets").Id,
                "Tower Hamlets Organisation Professional person"),

        };

        return userOrganisations;
    }
    
}
