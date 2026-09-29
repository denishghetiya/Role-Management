using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RoleManagement.DBContext;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace RoleManagement.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CheckAccessAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int _pageId;
        private readonly string _permissionType;

        public CheckAccessAttribute(int pageId, string permissionType)
        {
            _pageId = pageId;
            _permissionType = permissionType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            var permissionsJson = user.FindFirst("Permissions")?.Value;

            if (string.IsNullOrEmpty(permissionsJson))
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

            var permissions = JsonSerializer.Deserialize<List<RolePermission>>(permissionsJson);
            var permission = permissions?.FirstOrDefault(p => p.PageId == _pageId);

            bool isAllowed = _permissionType.ToLower() switch
            {
                "add" => permission?.Add == true,
                "edit" => permission?.Edit == true,
                "delete" => permission?.Delete == true,
                "view" => permission?.View == true,
                "editpermission" => permission?.EditPermission == true,
                _ => false
            };

            if (!isAllowed)
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
