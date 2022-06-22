using Freelancer_s_Web.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Freelancer_s_Web.Utils
{
    public static class CustomAuthorization
    {
        public static LoginUserVM loginUser
        {
            get
            {
                IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
                return (_httpContextAccessor.HttpContext.Session.GetString("LoginUser") != null) ?
                            JsonUtils.DeserializeComplexData<LoginUserVM>(_httpContextAccessor.HttpContext.Session.GetString("LoginUser")) : null;
            }
        }
        public static void Login(LoginUserVM user)
        {
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            _httpContextAccessor.HttpContext.Session.SetString("LoginUser", JsonUtils.SerializeComplexData(user));
        }
    }
}
