using Ardalis.GuardClauses;
using AutoMapper;
using FamilyHubs.Organisation.Core.Dto;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OrganisationApi.Api.Commands.UpdateOpenReferralOrganisation;


public class UpdateOpenReferralOrganisationCommand : IRequest<string>
{
    public UpdateOpenReferralOrganisationCommand(string id, OpenReferralOrganisationExDto openReferralOrganisation)
    {
        Id = id;
        OpenReferralOrganisation = openReferralOrganisation;
    }

    public OpenReferralOrganisationExDto OpenReferralOrganisation { get; init; }

    public string Id { get; set; }
}

public class UpdateOpenReferralOrganisationCommandHandler : IRequestHandler<UpdateOpenReferralOrganisationCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UpdateOpenReferralOrganisationCommandHandler> _logger;

    public UpdateOpenReferralOrganisationCommandHandler(ApplicationDbContext context, ILogger<UpdateOpenReferralOrganisationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<string> Handle(UpdateOpenReferralOrganisationCommand request, CancellationToken cancellationToken)
    {

        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(request.OpenReferralOrganisation, nameof(OpenReferralOrganisation));

        var entity = await _context.OpenReferralOrganisations.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OpenReferralOrganisation), request.Id);
        }

        

        try
        {
            if (entity.OrganisationTypeEx.Id != request.OpenReferralOrganisation.OrganisationTypeId)
            {
                var organisationType = _context.OrganisationTypes.FirstOrDefault(x => x.Id == request.OpenReferralOrganisation.OrganisationTypeId);
                ArgumentNullException.ThrowIfNull(organisationType, nameof(organisationType));
                entity.OrganisationTypeEx = organisationType;
            }

            entity.Name = request.OpenReferralOrganisation.Name ?? string.Empty;
            entity.Description = request.OpenReferralOrganisation.Description;
            entity.Logo = request.OpenReferralOrganisation.Logo;
            entity.Uri = request.OpenReferralOrganisation.Uri;
            entity.Url = request.OpenReferralOrganisation.Url;
            entity.Email = request.OpenReferralOrganisation.Email;
            entity.ContactName = request.OpenReferralOrganisation.ContactName;
            entity.ContactPhone = request.OpenReferralOrganisation.ContactPhone;
            
            entity.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred updating organisation. {exceptionMessage}", ex.Message);
#pragma warning disable S112 // General exceptions should never be thrown
            throw new Exception(ex.Message, ex);
#pragma warning restore S112 // General exceptions should never be thrown
        }

        return entity.Id;
    }
}


