using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleManagement.DBContext;
using RoleManagement.Extensions;
using RoleManagement.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace RoleManagement.Controllers
{
    [Authorize]
    public class CustomerListController : Controller
    {
        private readonly RoleManagementContext _context;

        public CustomerListController(RoleManagementContext context)
        {
            _context = context;
        }
        [CheckAccess(3, "View")]
        public async Task<IActionResult> CustomerList()
        {
            var permission = User.GetPagePermission(3);

                var customer = await _context.CustomerLists
                .Select(s => new CustomerListViewModel
                {
                    CustomerId = s.CustomerId,
                    CustomerName = s.CustomerName,
                    ProductName = s.ProductName,
                    Amount = s.Amount
                }).ToListAsync();

                var model = new CustomerListPageViewModel
                {
                    Customers = customer,
                    Add = permission.Add,
                    Edit = permission.Edit,
                    Delete = permission.Delete,
                    EditPermission = permission.EditPermission,
                };
                return View(model);
        }
        [CheckAccess(3, "Add")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
                var model = new CustomerListViewModel();
                return View(model);
        }
        [CheckAccess(3, "Add")]
        [HttpPost]
        public async Task<IActionResult> Create(CustomerListViewModel model)
        {
                if (ModelState.IsValid)
                {
                    var customer = new CustomerList
                    {
                        CustomerName = model.CustomerName,
                        ProductName = model.ProductName,
                        Amount = model.Amount
                    };
                    _context.CustomerLists.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("CustomerList");
                }
                return View("Create", model);
        }
        [CheckAccess(3, "Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
                var customer = await _context.CustomerLists.FirstOrDefaultAsync(f=>f.CustomerId==id);
                if (customer == null) return NotFound();

                var model = new CustomerListViewModel
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    ProductName = customer.ProductName,
                    Amount = customer.Amount
                };
                return View("Create", model);
        }
        [CheckAccess(3, "Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerListViewModel model)
        {
                if (ModelState.IsValid)
                {
                    var customer = await _context.CustomerLists.FirstOrDefaultAsync(f=>f.CustomerId==model.CustomerId);
                    if (customer == null) return NotFound();

                    customer.CustomerName = model.CustomerName;
                    customer.ProductName = model.ProductName;
                    customer.Amount = model.Amount;

                    _context.CustomerLists.Update(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("CustomerList");
                }
                return View("Create", model);
        }
        [CheckAccess(3, "Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
                var customer = await _context.CustomerLists.FirstOrDefaultAsync(f=>f.CustomerId==id);
                if (customer == null) return NotFound();

                _context.CustomerLists.Remove(customer);
                await _context.SaveChangesAsync();
                //return RedirectToAction("CustomerList");
                return Ok();
        }
    }
}
