using FamilyHubs.Organisation.Core.Dto;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OrganisationApi.Api.Queries.ListOrganisation;

public class ListOpenReferralOrganisationCommand : IRequest<List<OpenReferralOrganisationExDto>>
{
    public ListOpenReferralOrganisationCommand()
    {

    }
}

public class ListOpenReferralOrganisationCommandHandler : IRequestHandler<ListOpenReferralOrganisationCommand, List<OpenReferralOrganisationExDto>>
{
    private readonly ApplicationDbContext _context;

    public ListOpenReferralOrganisationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OpenReferralOrganisationExDto>> Handle(ListOpenReferralOrganisationCommand request, CancellationToken cancellationToken)
    {
        var organisations = await _context.OpenReferralOrganisations.Select(org => new OpenReferralOrganisationExDto(
            org.Id,
            org.OrganisationTypeEx.Id,
            org.Name,
            org.Description,
            org.Logo,
            org.Uri,
            org.Url,
            org.Email,
            org.ContactName,
            org.ContactPhone
            )).ToListAsync(cancellationToken: cancellationToken);
        return organisations;
    }
}
