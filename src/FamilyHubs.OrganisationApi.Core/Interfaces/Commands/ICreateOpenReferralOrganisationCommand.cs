using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralOrganisations;

namespace FamilyHubs.Organisation.Core.Interfaces.Commands
{
    public interface ICreateOpenReferralOrganisationCommand
    {
        OpenReferralOrganisationDto OpenReferralOrganisation { get; init; }
    }
}