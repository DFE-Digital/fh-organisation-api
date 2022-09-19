using FamilyHubs.Organisation.Core.Interfaces.Entities;
using FamilyHubs.SharedKernel;
using FamilyHubs.SharedKernel.Interfaces;

namespace FamilyHubs.Organisation.Core.Entities;

public class OpenReferralOrganisation : EntityBase<string>, IOpenReferralOrganisation, IAggregateRoot
{
    public OpenReferralOrganisation() { }

    public OpenReferralOrganisation(
        string id,
        string name = default!,
        string? description = default!,
        string? logo = default!,
        string? uri = default!,
        string? url = default!
    )
    {
        Id = id;
        Name = name ?? default!;
        Description = description ?? string.Empty;
        Logo = logo ?? string.Empty;
        Uri = uri ?? string.Empty;
        Url = url ?? string.Empty;
    }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Logo { get; set; } = string.Empty;
    public string? Uri { get; set; } = string.Empty;
    public string? Url { get; set; } = string.Empty;

    public void Update(OpenReferralOrganisation openReferralOpenReferralOrganisation)
    {
        Name = openReferralOpenReferralOrganisation.Name;
        Description = openReferralOpenReferralOrganisation.Description;
        Logo = openReferralOpenReferralOrganisation.Logo;
        Uri = openReferralOpenReferralOrganisation.Uri;
        Url = openReferralOpenReferralOrganisation.Url;
    }
}

