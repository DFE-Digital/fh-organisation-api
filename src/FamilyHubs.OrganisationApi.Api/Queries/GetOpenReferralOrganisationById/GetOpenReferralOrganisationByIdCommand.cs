using Ardalis.GuardClauses;
using AutoMapper;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralOrganisations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OrganisationApi.Api.Queries.GetOpenReferralOrganisationById;


public class GetOpenReferralOrganisationByIdCommand : IRequest<OpenReferralOrganisationDto>
{
    public string Id { get; set; } = default!;
}

public class GetOpenReferralOrganisationByIdHandler : IRequestHandler<GetOpenReferralOrganisationByIdCommand, OpenReferralOrganisationDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOpenReferralOrganisationByIdHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OpenReferralOrganisationDto> Handle(GetOpenReferralOrganisationByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OpenReferralOrganisations
           .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OpenReferralOrganisation), request.Id);
        }

        var result = new OpenReferralOrganisationDto(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Logo,
            entity.Uri,
            entity.Url
        );

        return result;
    }
}


