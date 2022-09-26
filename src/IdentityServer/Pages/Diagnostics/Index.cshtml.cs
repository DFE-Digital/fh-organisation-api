using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace FamilyHubs.IdentityServerHost.Pages.Diagnostics;

[SecurityHeaders]
[Authorize]
public class Index : PageModel
{
    public ViewModel? View { get; set; } = default!;

    public async Task<IActionResult> OnGet()
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext?.Connection?.LocalIpAddress?.ToString() };
#pragma warning restore CS8601 // Possible null reference assignment.
        if (!localAddresses.Contains(HttpContext?.Connection?.RemoteIpAddress?.ToString()))
        {
            return NotFound();
        }

#pragma warning disable CS8604 // Possible null reference argument.
        View = new ViewModel(await HttpContext.AuthenticateAsync());
#pragma warning restore CS8604 // Possible null reference argument.

        return Page();
    }
}