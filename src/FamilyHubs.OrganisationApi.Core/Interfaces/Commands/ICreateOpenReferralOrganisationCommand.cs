using FamilyHubs.Organisation.Core.Dto;

namespace FamilyHubs.Organisation.Core.Interfaces.Commands
{
    public interface ICreateOpenReferralOrganisationCommand
    {
        OpenReferralOrganisationExDto OpenReferralOrganisation { get; init; }

    }
}