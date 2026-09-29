using System.Security.Claims;
using System.Text.Json;
using RoleManagement.DBContext; 

namespace RoleManagement.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static RolePermission GetPagePermission(this ClaimsPrincipal user, int pageId)
        {
            var permissionsJson = user.FindFirst("Permissions")?.Value;

            var allPermissions = JsonSerializer.Deserialize<List<RolePermission>>(permissionsJson);
            return allPermissions?.FirstOrDefault(p => p.PageId == pageId);
        }
    }
}
