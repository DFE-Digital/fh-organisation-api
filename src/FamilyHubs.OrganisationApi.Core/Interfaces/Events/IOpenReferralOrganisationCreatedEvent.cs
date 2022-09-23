using FamilyHubs.Organisation.Core.Entities;

namespace FamilyHubs.Organisation.Core.Interfaces.Events
{
    public interface IOpenReferralOrganisationCreatedEvent
    {
        OpenReferralOrganisationEx Item { get; }
    }
}