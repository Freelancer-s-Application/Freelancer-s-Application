using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text.Json.Serialization;
using System.Text.Json;

namespace Freelancer_s_Web.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Page("./Login", pageHandler: "GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnGetGoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync();

            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return new JsonResult(claims);
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("OnGet");
        }
    }
}
