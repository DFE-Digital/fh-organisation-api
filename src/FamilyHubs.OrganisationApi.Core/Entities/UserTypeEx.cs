using FamilyHubs.SharedKernel;

namespace FamilyHubs.Organisation.Core.Entities;

public class UserTypeEx : EntityBase<string>
{
    private UserTypeEx() { }
    public UserTypeEx(
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


