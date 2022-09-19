using Ardalis.GuardClauses;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Organisation.Api.Commands.UpdateOpenReferralOrganisation;


public class UpdateOpenReferralOrganisationCommand : IRequest<string>
{
    public UpdateOpenReferralOrganisationCommand(string id, OpenReferralOrganisation openReferralOrganisation)
    {
        Id = id;
        OpenReferralOrganisation = openReferralOrganisation;
    }

    public OpenReferralOrganisation OpenReferralOrganisation { get; init; }

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
#pragma warning disable S3236 // Caller information arguments should not be provided explicitly
        ArgumentNullException.ThrowIfNull(request, nameof(request));
#pragma warning restore S3236 // Caller information arguments should not be provided explicitly

        var entity = await _context.OpenReferralOrganisations.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OpenReferralOrganisation), request.Id);
        }

        try
        {
            entity.Update(request.OpenReferralOrganisation);

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


