using AutoMapper;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Core.Events;
using FamilyHubs.Organisation.Core.Interfaces.Commands;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralOrganisations;
using MediatR;

namespace FamilyHubs.OrganisationApi.Api.Commands.CreateOpenReferralOrganisation;

public class CreateOpenReferralOrganisationCommand : IRequest<string>, ICreateOpenReferralOrganisationCommand
{
    public CreateOpenReferralOrganisationCommand(OpenReferralOrganisationDto openReferralOrganisation)
    {
        OpenReferralOrganisation = openReferralOrganisation;
    }

    public OpenReferralOrganisationDto OpenReferralOrganisation { get; init; }
}

public class CreateOpenReferralOrganisationCommandHandler : IRequestHandler<CreateOpenReferralOrganisationCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOpenReferralOrganisationCommandHandler> _logger;

    public CreateOpenReferralOrganisationCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<CreateOpenReferralOrganisationCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<string> Handle(CreateOpenReferralOrganisationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<OpenReferralOrganisationEx>(request.OpenReferralOrganisation);
#pragma warning disable S3236 // Caller information arguments should not be provided explicitly
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
#pragma warning restore S3236 // Caller information arguments should not be provided explicitly

            entity.RegisterDomainEvent(new OpenReferralOrganisationCreatedEvent(entity));

            _context.OpenReferralOrganisations.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating organisation. {exceptionMessage}", ex.Message);
#pragma warning disable S112 // General exceptions should never be thrown
            throw new Exception(ex.Message, ex);
#pragma warning restore S112 // General exceptions should never be thrown
        }

        if (request is not null && request.OpenReferralOrganisation is not null)
            return request.OpenReferralOrganisation.Id;
        else
            return string.Empty;
    }
}
