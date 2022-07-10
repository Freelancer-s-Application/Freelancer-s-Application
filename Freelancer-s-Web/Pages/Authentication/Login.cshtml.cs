using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text.Json.Serialization;
using System.Text.Json;
using System.Security.Claims;
using Freelancer_s_Web.Utils;
using Microsoft.AspNetCore.Http;
using Freelancer_s_Web.Commons;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.ViewModel;
using Freelancer_s_Web.Models;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
namespace Freelancer_s_Web.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        private readonly ILogger<LoginModel> _logger;
        public LoginModel(UnitOfWorkFactory unitOfWorkFactory, ILogger<LoginModel> logger)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _logger = logger;
        }
        public IActionResult OnGetLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Page("./Login", pageHandler: "GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnGetGoogleResponse()
        {
            var authProperties = new AuthenticationProperties
            {
            };
            var result = await HttpContext.AuthenticateAsync();

            var claims = result.Principal.Identities.FirstOrDefault().Claims;
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);
            string email = principle.FindFirstValue(ClaimTypes.Email);
            string displayName = principle.FindFirstValue(ClaimTypes.Name);
            string avatar = principle.FindFirstValue("urn:google:picture");
            if (email == AppConfiguration.GetAdminEmail())
            {
                CustomAuthorization.Login(new LoginUserVM()
                {
                    DisplayName = displayName,
                    Id = 0,
                    Email = email,
                    Avatar = avatar,
                    Role = CommonEnums.ROLE.ADMINISTRATOR,
                });
                //await HttpContext.SignInAsync(
                //    CookieAuthenticationDefaults.AuthenticationScheme,
                //    new ClaimsPrincipal(identity),
                //    authProperties);
                return RedirectToPage("/Index");
            }
            else
            {
                using (var work = _unitOfWorkFactory.Get)
                {
                    User user = work.UserRepository.GetFirstOrDefault(p => p.Email.Equals(email));
                    if (user == null) // first time login
                    {
                        User logging = new User()
                        {
                            DisplayName = displayName,
                            Email = email,
                            Avatar = avatar,
                            CreatedBy = email,
                            CreatedAt = DateTime.Now,
                        };
                        int id = work.UserRepository.CreateUser(logging);
                        CustomAuthorization.Login(new LoginUserVM()
                        {
                            DisplayName = displayName,
                            Id = id,
                            Email = email,
                            Avatar = avatar,
                            Role = CommonEnums.ROLE.USER,
                        });
                    }
                    else
                    {   if (user.IsDeleted)
                        {
                            TempData["Error"] = "Your account has been deactivated!! Please contact administrator for more information!";
                            return Redirect("/Index");
                        }
                        CustomAuthorization.Login(new LoginUserVM()
                        {
                            DisplayName = displayName,
                            Id = user.Id,
                            Email = email,
                            Avatar = avatar,
                            Role = CommonEnums.ROLE.USER,
                        });
                    }
                }
                //await HttpContext.SignInAsync(
                //    CookieAuthenticationDefaults.AuthenticationScheme,
                //    new ClaimsPrincipal(identity),
                //    authProperties);
                return RedirectToPage("/Index");
            }
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return Redirect("/Index");
        }
    }
}
