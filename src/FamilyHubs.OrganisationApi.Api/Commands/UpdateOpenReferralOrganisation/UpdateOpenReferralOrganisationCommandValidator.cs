using FluentValidation;

namespace FamilyHubs.Organisation.Api.Commands.UpdateOpenReferralOrganisation;

public class UpdateOpenReferralOrganisationCommandValidator : AbstractValidator<UpdateOpenReferralOrganisationCommand>
{
    public UpdateOpenReferralOrganisationCommandValidator()
    {
        RuleFor(v => v.Id)
            .MinimumLength(1)
            .MaximumLength(50)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.OpenReferralOrganisation)
            .NotNull();

        RuleFor(v => v.OpenReferralOrganisation.Id)
            .MinimumLength(1)
            .MaximumLength(50)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.OpenReferralOrganisation.Name)
            .MinimumLength(1)
            .MaximumLength(50)
            .NotNull()
            .NotEmpty();
    }
}
