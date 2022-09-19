using FamilyHubs.Organisation.Core.Entities;

namespace FamilyHubs.Organisation.Core.Interfaces.Entities;

public interface IOpenReferralOrganisation : IEntityBase<string>
{
    string? Description { get; }
    string? Logo { get; }
    string Name { get; }
    string? Uri { get; }
    string? Url { get; }

    void Update(OpenReferralOrganisation openReferralOpenReferralOrganisation);
}