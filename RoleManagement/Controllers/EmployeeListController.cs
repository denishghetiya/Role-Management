using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleManagement.DBContext;
using RoleManagement.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using RoleManagement.Extensions;

namespace RoleManagement.Controllers
{
    [Authorize]
    public class EmployeeListController : Controller
    {
        private readonly RoleManagementContext _context;

        public EmployeeListController(RoleManagementContext context)
        {
            _context = context;
        }
        [CheckAccess(2, "View")]
        public async Task<IActionResult> EmployeeList()
        {
            var permission = User.GetPagePermission(2);

                var employees = await _context.EmployeeLists
                .Select(s => new EmployeeListViewModel
                {
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.EmployeeName,
                    DepartmentName = s.DepartmentName
                }).ToListAsync();

                var model = new EmployeeListPageViewModel
                {
                    Employees = employees,
                    Add = permission.Add,
                    Edit = permission.Edit,
                    Delete = permission.Delete,
                    EditPermission = permission.EditPermission,
                };

                return View(model);
        }
        [CheckAccess(2, "Add")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
                var model = new EmployeeListViewModel();
                return View(model);
        }
        [CheckAccess(2, "Add")]
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeListViewModel model)
        {
                if (ModelState.IsValid)
                {
                    var employees = new EmployeeList
                    {
                        EmployeeName = model.EmployeeName,
                        DepartmentName = model.DepartmentName
                    };
                    _context.EmployeeLists.Add(employees);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("EmployeeList");
                }
                return View("Create", model);
        }
        [CheckAccess(2, "Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
                var employee = await _context.EmployeeLists.FirstOrDefaultAsync(f=>f.EmployeeId==id);
                if (employee == null) return NotFound();

                var model = new EmployeeListViewModel
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    DepartmentName = employee.DepartmentName
                };
                return View("Create", model);
        }
        [CheckAccess(2, "Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeListViewModel model)
        {
                if (ModelState.IsValid)
                {
                    var employee = await _context.EmployeeLists.FirstOrDefaultAsync(f=>f.EmployeeId==model.EmployeeId);
                    if (employee == null) return NotFound();

                    employee.EmployeeName = model.EmployeeName;
                    employee.DepartmentName = model.DepartmentName;

                    _context.EmployeeLists.Update(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("EmployeeList");
                }
                return View("Create", model);
        }
        [CheckAccess(2, "Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
                var employee = await _context.EmployeeLists.FirstOrDefaultAsync(f=>f.EmployeeId==id);
                if (employee == null) return NotFound();

                _context.EmployeeLists.Remove(employee);
                await _context.SaveChangesAsync();
                //return RedirectToAction("EmployeeList");
                return Ok();
        }
    }
}
