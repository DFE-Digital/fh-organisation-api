namespace FamilyHubs.Organisation.Core.Entities;

public class OpenReferralOrganisationEx : OpenReferralOrganisation
{
    private OpenReferralOrganisationEx() { }
    public OpenReferralOrganisationEx(string id,
        string name = default!,
        string? description = default!,
        string? logo = default!,
        string? uri = default!,
        string? url = default!,
        string? email = default!,
        string? contactName = default!,
        string? contactPhone = default!,
        OrganisationTypeEx organisationTypeEx = default!)
        : base(id,name,description,logo,uri,url)
    {
        Email = email;
        ContactName = contactName;
        ContactPhone = contactPhone;
        OrganisationTypeEx = organisationTypeEx;
    }

    public string? Email { get; set; } = default!;
    public string? ContactName { get; set; } = default!;
    public string? ContactPhone { get; set; } = default!;
    public OrganisationTypeEx OrganisationTypeEx { get; set; } = default!;
}
