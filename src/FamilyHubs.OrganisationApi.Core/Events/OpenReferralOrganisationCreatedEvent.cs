using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Core.Interfaces.Events;
using FamilyHubs.SharedKernel;

namespace FamilyHubs.Organisation.Core.Events;

public class OpenReferralOrganisationCreatedEvent : DomainEventBase, IOpenReferralOrganisationCreatedEvent
{
    public OpenReferralOrganisationCreatedEvent(OpenReferralOrganisationEx item)
    {
        Item = item;
    }

    public OpenReferralOrganisationEx Item { get; }
}
