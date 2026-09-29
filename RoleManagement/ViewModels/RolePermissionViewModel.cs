namespace RoleManagement.ViewModels
{
    public class RolePermissionViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;

        public List<RolePermissionDetail> Permissions { get; set; } = new();
    }

    public class RolePermissionDetail
    {
        public int PageId { get; set; }
        public string PageName { get; set; } = string.Empty;

        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public bool EditPermission { get; set; }
    }

}
