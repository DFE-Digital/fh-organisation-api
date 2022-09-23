using AutoMapper;
using FamilyHubs.Organisation.Core.Dto;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.Organisation.Core.Events;
using FamilyHubs.Organisation.Core.Interfaces.Commands;
using FamilyHubs.Organisation.Infrastructure.Persistence.Repository;
using MediatR;

namespace FamilyHubs.OrganisationApi.Api.Commands.CreateOpenReferralOrganisation;

public class CreateOpenReferralOrganisationCommand : IRequest<string>, ICreateOpenReferralOrganisationCommand
{
    public CreateOpenReferralOrganisationCommand(OpenReferralOrganisationExDto openReferralOrganisation)
    {
        OpenReferralOrganisation = openReferralOrganisation;
    }

    public OpenReferralOrganisationExDto OpenReferralOrganisation { get; init; }
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
            var organisationType = _context.OrganisationTypes.FirstOrDefault(x => x.Id == request.OpenReferralOrganisation.OrganisationTypeId);
            ArgumentNullException.ThrowIfNull(organisationType, nameof(organisationType));
            var entity = _mapper.Map<OpenReferralOrganisationEx>(request.OpenReferralOrganisation);
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            entity.OrganisationTypeEx = organisationType;

            entity.RegisterDomainEvent(new OpenReferralOrganisationCreatedEvent(entity));

            _context.OpenReferralOrganisations.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating organisation. {exceptionMessage}", ex.Message);
            throw new Exception(ex.Message, ex);
        }

        if (request is not null && request.OpenReferralOrganisation is not null)
            return request.OpenReferralOrganisation.Id;
        else
            return string.Empty;
    }
}
