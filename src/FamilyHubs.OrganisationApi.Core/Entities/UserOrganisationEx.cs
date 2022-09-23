using FamilyHubs.SharedKernel;

namespace FamilyHubs.Organisation.Core.Entities;

public class UserOrganisationEx : EntityBase<string>
{
    public UserOrganisationEx(string id, string organisationId, string description)
    {
        Id = id;
        OrganisationId = organisationId;
        Description = description;
    }

    public string OrganisationId { get; set; } = default!;
    public string Description { get; set; } = default!;
}
