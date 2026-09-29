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
    public class StudentListController : Controller
    {
        private readonly RoleManagementContext _context;

        public StudentListController(RoleManagementContext context)
        {
            _context = context;
        }

        [CheckAccess(1, "View")]
        public async Task<IActionResult> StudentList()
        {
            var permission = User.GetPagePermission(1);

            var students = await _context.StudentLists
                    .Select(s => new StudentListViewModel
                    {
                        StudentId = s.StudentId,
                        StudentName = s.StudentName,
                        RollNumber = s.RollNumber,
                        Class = s.Class
                    }).ToListAsync();

                var model = new StudentListPageViewModel
                {
                    Students = students,
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
                var model = new StudentListViewModel();
                return View(model);
        }

        [CheckAccess(1, "Add")]
        [HttpPost]
        public async Task<IActionResult> Create(StudentListViewModel model)
        {
                if (ModelState.IsValid)
                {
                    var student = new StudentList
                    {
                        StudentName = model.StudentName,
                        RollNumber = model.RollNumber,
                        Class = model.Class
                    };
                    _context.StudentLists.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("StudentList");
                }
                return View("Create", model);
        }

        [CheckAccess(1, "Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
                var student = await _context.StudentLists.FirstOrDefaultAsync(f=>f.StudentId==id);
                if (student == null) return NotFound();

                var model = new StudentListViewModel
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    RollNumber = student.RollNumber,
                    Class = student.Class
                };
                return View("Create", model);
        }

        [CheckAccess(1, "Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(StudentListViewModel model)
        {
                if (ModelState.IsValid)
                {
                    var student = await _context.StudentLists.FirstOrDefaultAsync(f=>f.StudentId==model.StudentId);
                    if (student == null) return NotFound();

                    student.StudentName = model.StudentName;
                    student.RollNumber = model.RollNumber;
                    student.Class = model.Class;

                    _context.StudentLists.Update(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("StudentList");
                }
                return View("Create", model);
        }

        [CheckAccess(1, "Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
                var student = await _context.StudentLists.FirstOrDefaultAsync(f=>f.StudentId==id);
                if (student == null) return NotFound();

                _context.StudentLists.Remove(student);
                await _context.SaveChangesAsync();
                //return RedirectToAction("StudentList");
                return Ok();
        }
    }
}
