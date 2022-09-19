using FluentValidation;

namespace FamilyHubs.Organisation.Api.Queries.GetOpenReferralOrganisationById;
public class GetOpenReferralOrganisationByIdCommandValidator : AbstractValidator<GetOpenReferralOrganisationByIdCommand>
{
    public GetOpenReferralOrganisationByIdCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .NotEmpty();
    }
}