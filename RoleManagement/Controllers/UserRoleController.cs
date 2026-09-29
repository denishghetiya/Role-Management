using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleManagement.DBContext;
using RoleManagement.ViewModels;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using RoleManagement.Extensions;


namespace RoleManagement.Controllers
{
    [Authorize]
    public class UserRoleController : Controller
    {
        private readonly RoleManagementContext _context;

        public UserRoleController(RoleManagementContext context)
        {
            _context = context;
        }

        [CheckAccess(5, "View")]
        public async Task<IActionResult> UserRole()
        {
            var permission = User.GetPagePermission(5);

            var roles = await _context.UserRoles
                    .Select(s => new UserRoleListViewModel
                    {
                        RoleId = s.RoleId,
                        RoleName = s.RoleName
                    }).ToListAsync();

            var model = new UserRoleListPageViewModel
            {
                UserRoles = roles,
                Add = permission.Add,
                Edit = permission.Edit,
                Delete = permission.Delete,
                EditPermission = permission.EditPermission,
            };

            return View(model);
        }

        [CheckAccess(1, "Add")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new UserRoleListViewModel();
            return View(model);
        }

        [CheckAccess(1, "Add")]
        [HttpPost]
        public async Task<IActionResult> Create(UserRoleListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new UserRole
                {
                    RoleName = model.RoleName
                };
                _context.UserRoles.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserRole");
            }
            return View("Create", model);
        }

        [CheckAccess(1, "Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var role = await _context.UserRoles.FirstOrDefaultAsync(f=>f.RoleId==id);
            if (role == null) return NotFound();

            var model = new UserRoleListViewModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
            return View("Create", model);
        }

        [CheckAccess(1, "Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _context.UserRoles.FirstOrDefaultAsync(f=>f.RoleId==model.RoleId);
                if (role == null) return NotFound();

                role.RoleName = model.RoleName;

                _context.UserRoles.Update(role);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserRole");
            }
            return View("Create", model);
        }

        [CheckAccess(1, "Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _context.UserRoles.FirstOrDefaultAsync(f=>f.RoleId==id);
            if (role == null) return NotFound();

            _context.UserRoles.Remove(role);
            await _context.SaveChangesAsync();
            //return RedirectToAction("UserRole");
            return Ok();
        }
    }
}
