// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;

namespace FamilyHubs.IdentityServerHost.Pages.Consent;

public class InputModel
{
    public string Button { get; set; } = default!;
    public IEnumerable<string> ScopesConsented { get; set; } = default!;
    public bool RememberConsent { get; set; } = true;
    public string ReturnUrl { get; set; } = default!;
    public string Description { get; set; } = default!;
}