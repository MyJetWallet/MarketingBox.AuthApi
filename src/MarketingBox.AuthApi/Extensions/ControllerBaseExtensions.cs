using MarketingBox.AuthApi.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AuthApi.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static string GetTenantId(this ControllerBase controllerBase)
        {
            return controllerBase.User.GetTenantId();
        }
    }
}
