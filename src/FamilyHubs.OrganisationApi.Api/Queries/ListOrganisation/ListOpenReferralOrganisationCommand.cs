using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralOrganisations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OrganisationApi.Api.Queries.ListOrganisation;

public class ListOpenReferralOrganisationCommand : IRequest<List<OpenReferralOrganisationDto>>
{
    public ListOpenReferralOrganisationCommand()
    {

    }
}

public class ListOpenReferralOrganisationCommandHandler : IRequestHandler<ListOpenReferralOrganisationCommand, List<OpenReferralOrganisationDto>>
{
    private readonly ApplicationDbContext _context;

    public ListOpenReferralOrganisationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OpenReferralOrganisationDto>> Handle(ListOpenReferralOrganisationCommand request, CancellationToken cancellationToken)
    {
        var organisations = await _context.OpenReferralOrganisations.Select(org => new OpenReferralOrganisationDto(
            org.Id,
            org.Name,
            org.Description,
            org.Logo,
            org.Uri,
            org.Url
            )).ToListAsync();
        return organisations;
    }
}
