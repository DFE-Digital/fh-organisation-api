using AutoMapper;
using FamilyHubs.Organisation.Api.Commands.CreateOpenReferralOrganisation;
using FamilyHubs.Organisation.Api.Commands.UpdateOpenReferralOrganisation;
using FamilyHubs.Organisation.Api.Queries.GetOpenReferralOrganisationById;
using FamilyHubs.Organisation.Api.Queries.ListOrganisation;
using FamilyHubs.Organisation.Core.Entities;
using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralOrganisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FamilyHubs.Organisation.Api.Endpoints;

public class MinimalOrganisationEndPoints
{
    public void RegisterOrganisationEndPoints(WebApplication app)
    {
        app.MapPost("api/organizations", async ([FromBody] OpenReferralOrganisationDto request, CancellationToken cancellationToken, ISender _mediator, ILogger<MinimalOrganisationEndPoints> logger) =>
        {
            try
            {
                CreateOpenReferralOrganisationCommand command = new(request);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred creating organisation (api). {exceptionMessage}", ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Organisations", "Create Organisation") { Tags = new[] { "Organisations" } });

        app.MapGet("api/organizations/{id}", async (string id, CancellationToken cancellationToken, ISender _mediator, ILogger<MinimalOrganisationEndPoints> logger) =>
        {
            try
            {
                GetOpenReferralOrganisationByIdCommand request = new()
                {
                    Id = id
                };
                var result = await _mediator.Send(request, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred getting organisation (api). {exceptionMessage}", ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Get Organisation", "Get Organisation By Id") { Tags = new[] { "Organisations" } });

        app.MapGet("api/organizations", async (CancellationToken cancellationToken, ISender _mediator, ILogger<MinimalOrganisationEndPoints> logger) =>
        {
            try
            {
                ListOpenReferralOrganisationCommand request = new();
                var result = await _mediator.Send(request, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred listing organisation (api). {exceptionMessage}", ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("List Organisations", "List Organisations") { Tags = new[] { "Organisations" } });

        app.MapPut("api/organizations/{id}", async (string id, [FromBody] OpenReferralOrganisationWithServicesDto request, CancellationToken cancellationToken, ISender _mediator, IMapper mapper, ILogger<MinimalOrganisationEndPoints> logger) =>
        {
            try
            {
                OpenReferralOrganisation openReferralOrganisation = mapper.Map<OpenReferralOrganisation>(request);
                UpdateOpenReferralOrganisationCommand command = new(id, openReferralOrganisation);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred updating organisation (api). {exceptionMessage}", ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Update Organisation", "Update Organisation By Id") { Tags = new[] { "Organisations" } });
    }
}
