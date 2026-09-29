namespace RoleManagement.ViewModels
{
    public class UserRoleListPageViewModel
    {
        public List<UserRoleListViewModel> UserRoles { get; set; } = new();
        public bool Add { get; set; }

        public bool Edit { get; set; }
        public bool EditPermission { get; set; }

        public bool Delete { get; set; }

    }

}
