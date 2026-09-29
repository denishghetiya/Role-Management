using Microsoft.AspNetCore.Mvc.Rendering;

namespace RoleManagement.ViewModels
{
    public class CreateUserViewModel
    {
        public int? UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public List<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();
    }


}
