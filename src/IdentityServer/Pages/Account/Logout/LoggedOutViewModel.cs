
// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace FamilyHubs.IdentityServerHost.Pages.Logout;

public class LoggedOutViewModel
{
    public string PostLogoutRedirectUri { get; set; } = default!;
    public string ClientName { get; set; } = default!;
    public string SignOutIframeUrl { get; set; } = default!;
    public bool AutomaticRedirectAfterSignOut { get; set; }
}