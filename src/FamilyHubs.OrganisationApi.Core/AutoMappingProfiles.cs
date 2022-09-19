using AutoMapper;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralOrganisations;

namespace FamilyHubs.Organisation.Core;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<OpenReferralOrganisationDto, OpenReferralOrganisation>();
    }
}
