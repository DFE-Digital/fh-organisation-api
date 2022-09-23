using FamilyHubs.SharedKernel;

namespace FamilyHubs.Organisation.Core.Entities;

public class UserEx : EntityBase<string>
{
    private UserEx() { }
    public UserEx(
        string id,
        UserTypeEx userTypeEx,
        string name = default!,
        string? description = default!,
        string? email = default!,
        string? contactName = default!,
        string? contactPhone = default!
    )
    {
        Id = id;
        Name = name ?? default!;
        Description = description ?? string.Empty;
        Email = email;
        ContactName = contactName;
        ContactPhone = contactPhone;
    }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Email { get; set; } = default!;
    public string? ContactName { get; set; } = default!;
    public string? ContactPhone { get; set; } = default!;
}


