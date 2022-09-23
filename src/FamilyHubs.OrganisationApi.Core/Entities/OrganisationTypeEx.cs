using FamilyHubs.SharedKernel;

namespace FamilyHubs.Organisation.Core.Entities;

public class OrganisationTypeEx : EntityBase<string>
{
    private OrganisationTypeEx() { }
    public OrganisationTypeEx(
        string id,
        string name = default!,
        string? description = default!
    )
    {
        Id = id;
        Name = name ?? default!;
        Description = description ?? string.Empty;
    }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
