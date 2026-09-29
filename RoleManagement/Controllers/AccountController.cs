using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using RoleManagement.DBContext;
using RoleManagement.ViewModel;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Security.Claims;

namespace RoleManagement.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly RoleManagementContext _context;

        public AccountController(RoleManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = (from u in _context.Users
                        where u.Email == model.Email && u.Password == model.Password
                        join p in _context.RolePermissions on u.UserId equals p.UserId into perms
                        select new
                        {
                            u.UserId,
                            u.Username,
                            u.Email,
                            u.Role,
                            u.IsActive,
                            Permissions = perms
                        }).AsQueryable() 
                           .Select(u => new
                           {
                               u.UserId,
                               u.Username,
                               u.Email,
                               u.Role,
                               u.IsActive,
                               Permissions = u.Permissions.Select(per => new
                               {
                                   per.PageId,
                                   per.Add,
                                   per.Edit,
                                   per.Delete,
                                   per.View,
                                   per.EditPermission
                               }).ToList()
                           }).FirstOrDefault();

            if (user == null) return RedirectToAction("Login");
            string permissionsJson = JsonSerializer.Serialize(user.Permissions);

            if (user != null)
            {
                if (!user.IsActive)
                {
                    ModelState.AddModelError("", "Your account is deactivated. Please contact admin.");
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserID", user.UserId.ToString()),
                    new Claim("Permissions", permissionsJson)

                };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal);

                return RedirectToAction("Dashboard", "Dashboard");

            }

            ModelState.AddModelError("", "Invalid Email or Password.");
            return View(model);
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");

            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied() => View();
    }
}


