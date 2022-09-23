using AutoMapper;
using FamilyHubs.Organisation.Core.Dto;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralOrganisations;

namespace FamilyHubs.Organisation.Core;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<OpenReferralOrganisationExDto, OpenReferralOrganisationEx>();
        CreateMap<OpenReferralOrganisationDto, OpenReferralOrganisation>();
    }
}
