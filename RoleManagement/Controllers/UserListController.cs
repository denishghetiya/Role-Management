using Microsoft.AspNetCore.Mvc;
using RoleManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using RoleManagement.DBContext;
using RoleManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using RoleManagement.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RoleManagement.Controllers
{
    [Authorize]
    public class UserListController : Controller
    {
        private readonly RoleManagementContext _context;

        public UserListController(RoleManagementContext context)
        {
            _context = context;
        }
        [CheckAccess(4, "View")]
        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var permission = User.GetPagePermission(4);

            var users = await _context.Users
                    .Select(s => new CreateUserViewModel
                    {
                        UserId = s.UserId,
                        Username = s.Username,
                        Email = s.Email,
                        Password = s.Password,
                        Role = s.Role,
                        IsActive = s.IsActive,
                    }).ToListAsync();

            var model = new CreateUserListPageViewModel
            {
                Users = users,
                Add = permission.Add,
                Edit = permission.Edit,
                Delete = permission.Delete,
                EditPermission = permission.EditPermission,
            };

            return View(model);
        }
        [CheckAccess(4, "Add")]
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            var roles = await _context.UserRoles
                .Select(r => new SelectListItem
                {
                    Value = r.RoleName, 
                    Text = r.RoleName
                })
                .ToListAsync();

            var model = new CreateUserViewModel
            {
                RoleList = roles
            };

            return View(model);
        }
        [CheckAccess(4, "Add")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
                if (!ModelState.IsValid)
                    return View(model);

                var newUser = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role,
                    IsActive = true
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                var allPages = await _context.PageNames.ToListAsync();

                var defaultPermissions = allPages.Select(p => new RolePermission
                {
                    UserId = newUser.UserId,
                    PageId = p.PageId,
                    Add = false,
                    Edit = false,
                    Delete = false,
                    View = false,
                    EditPermission = false
                }).ToList();

                _context.RolePermissions.AddRange(defaultPermissions);
                await _context.SaveChangesAsync();

                return RedirectToAction("UserList");
        
        }
        [CheckAccess(4, "EditPermission")]
        [HttpGet]
        public async Task<IActionResult> EditUserPermissions(int userId)
        {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null) return NotFound();

                var permissions = await _context.RolePermissions
                    .Where(rp => rp.UserId == userId)
                    .Include(rp => rp.Page)
                    .ToListAsync();

                var viewModel = new RolePermissionViewModel
                {
                    UserId = userId,
                    Username = user.Username,
                    Permissions = permissions.Select(rp => new RolePermissionDetail
                    {
                        PageId = rp.PageId,
                        PageName = rp.Page.PageName1,
                        Add = rp.Add,
                        Edit = rp.Edit,
                        Delete = rp.Delete,
                        View = rp.View,
                        EditPermission = rp.EditPermission
                    }).ToList()
                };

                return View(viewModel);
            
        }
        [CheckAccess(4, "EditPermission")]
        [HttpPost]
        public async Task<IActionResult> EditUserPermissions(int userId, RolePermissionViewModel model)
        {
                foreach (var item in model.Permissions)
                {
                    var existingPermission = await _context.RolePermissions
                        .FirstOrDefaultAsync(rp => rp.UserId == model.UserId && rp.PageId == item.PageId);

                    if (existingPermission != null)
                    {
                        existingPermission.Add = item.Add;
                        existingPermission.Edit = item.Edit;
                        existingPermission.Delete = item.Delete;
                        existingPermission.View = item.View;
                        existingPermission.EditPermission = item.EditPermission;

                        _context.RolePermissions.Update(existingPermission);
                    }
                    else
                    {
                        var newPermission = new RolePermission
                        {
                            UserId = model.UserId,
                            PageId = item.PageId,
                            Add = item.Add,
                            Edit = item.Edit,
                            Delete = item.Delete,
                            View = item.View,
                            EditPermission = item.EditPermission
                        };

                        _context.RolePermissions.Add(newPermission);
                    }
                }

                await _context.SaveChangesAsync();

                int userIdd = int.Parse(User.FindFirst("UserID")?.Value ?? "0");
                if (userIdd == userId) return RedirectToAction("Login", "Account");

                return RedirectToAction("UserList", new { userId = model.UserId });
         
        }
        [CheckAccess(4, "Edit")]
        [HttpGet]
        public async Task<IActionResult> EditUser(int userId)
        {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null) return NotFound();

                var model = new CreateUserViewModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role,
                    IsActive = user.IsActive
                };

                ViewBag.UserId = userId;
                return View("EditUser", model);
        
        }
        [CheckAccess(4, "Edit")]
        [HttpPost]
        public async Task<IActionResult> EditUser(int userId, CreateUserViewModel model)
        {
                if (!ModelState.IsValid) return View(model);

                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null) return NotFound();

                user.Username = model.Username;
                user.Email = model.Email;
                user.Password = model.Password;
                user.Role = model.Role;
                user.IsActive = model.IsActive;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                int userIdd = int.Parse(User.FindFirst("UserID")?.Value ?? "0");
                if (userIdd == userId) return RedirectToAction("Login", "Account");

                return RedirectToAction("UserList");
        
        }
        [CheckAccess(4, "Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null) return NotFound();

                var permissions = _context.RolePermissions.Where(rp => rp.UserId == userId);
                _context.RolePermissions.RemoveRange(permissions);

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                //return RedirectToAction("UserList");
                return Ok();
        
        }
    }
}
