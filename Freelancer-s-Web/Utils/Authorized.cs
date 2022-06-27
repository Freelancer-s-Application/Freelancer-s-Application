using Freelancer_s_Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Freelancer_s_Web.Utils
{
    public class Authorized : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string allowedroles;
        public Authorized(string roles)
        {
            this.allowedroles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Boolean check = false;
            if (CustomAuthorization.loginUser != null) 
            {
                var listRole = allowedroles.Split(',');
                foreach (string role in listRole)
                {
                    if (CustomAuthorization.loginUser.Role == role)
                    {
                        check = true;
                        break;
                    }
                }
            }
            if (!check)
            {
                context.Result = new RedirectResult("/Authentication/Unauthorized");
            }
            return;
        }
    }
}
