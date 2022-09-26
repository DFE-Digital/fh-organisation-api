using System.Collections.Generic;

namespace FamilyHubs.IdentityServerHost.Pages.Device;

public class ViewModel
{
    public string ClientName { get; set; } = default!;
    public string ClientUrl { get; set; } = default!;
    public string ClientLogoUrl { get; set; } = default!;
    public bool AllowRememberConsent { get; set; } = default!;

    public IEnumerable<ScopeViewModel> IdentityScopes { get; set; } = default!;
    public IEnumerable<ScopeViewModel> ApiScopes { get; set; } = default!;
}

public class ScopeViewModel
{
    public string Value { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool Emphasize { get; set; } = default!;
    public bool Required { get; set; } = default!;
    public bool Checked { get; set; } = default!;
}