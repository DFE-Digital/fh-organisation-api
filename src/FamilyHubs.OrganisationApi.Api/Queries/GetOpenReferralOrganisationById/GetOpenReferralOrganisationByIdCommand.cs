using Ardalis.GuardClauses;
using AutoMapper;
using FamilyHubs.Organisation.Core.Dto;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OrganisationApi.Api.Queries.GetOpenReferralOrganisationById;


public class GetOpenReferralOrganisationByIdCommand : IRequest<OpenReferralOrganisationExDto>
{
    public string Id { get; set; } = default!;
}

public class GetOpenReferralOrganisationByIdHandler : IRequestHandler<GetOpenReferralOrganisationByIdCommand, OpenReferralOrganisationExDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOpenReferralOrganisationByIdHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OpenReferralOrganisationExDto> Handle(GetOpenReferralOrganisationByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OpenReferralOrganisations
           .Include(x => x.OrganisationTypeEx)
           .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OpenReferralOrganisation), request.Id);
        }

        var result = new OpenReferralOrganisationExDto(
            entity.Id,
            entity.OrganisationTypeEx.Id,
            entity.Name,
            entity.Description,
            entity.Logo,
            entity.Uri,
            entity.Url,
            entity.Email,
            entity.ContactName,
            entity.ContactPhone
        );

        return result;
    }
}


