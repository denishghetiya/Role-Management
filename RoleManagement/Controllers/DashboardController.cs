using Microsoft.AspNetCore.Mvc;
using RoleManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using RoleManagement.DBContext;
using RoleManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace RoleManagement.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly RoleManagementContext _context;

        public DashboardController(RoleManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            return View("Dashboard");
        }

    }
}
