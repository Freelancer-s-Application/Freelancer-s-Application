using Freelancer_s_Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Freelancer_s_Web.Utils
{
    public class Authorized : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] allowedroles;
        public Authorized(params string[] roles)
        {
            this.allowedroles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (CustomAuthorization.loginUser == null) 
            {
                context.Result = new UnauthorizedResult();
            } else
            {
                foreach (string role in allowedroles)
                {
                    if (CustomAuthorization.loginUser.Role != role)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
            return;
        }
    }
}
