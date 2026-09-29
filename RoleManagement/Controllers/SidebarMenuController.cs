using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleManagement.DBContext;

namespace RoleManagement.Controllers
{
    [Authorize]
    public class SidebarMenuController : Controller
    {
        private readonly RoleManagementContext _context;

        public SidebarMenuController(RoleManagementContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetSidebarPages()
        {
            int userId = int.Parse(User.FindFirst("UserID")?.Value ?? "0");
            if (userId == 0) return RedirectToAction("Login", "Account");

            var pages = await _context.RolePermissions
                .Include(rp => rp.Page)
                .Where(rp => rp.UserId == userId && rp.View == true)
                .Select(rp => new
                {
                    pageId = rp.Page.PageId,
                    pageName = rp.Page.PageName1,
                    controller = rp.Page.Controller,
                    action = rp.Page.Action
                })
                .ToListAsync();

            return Json(pages);

        }
    }
}
