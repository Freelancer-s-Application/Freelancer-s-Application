using Freelancer_s_Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Freelancer_s_Web.Pages.Authentication
{
    [Authorized("ADMIN,USER")]
    public class TestModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
