namespace RoleManagement.ViewModels
{
    public class CreateUserListPageViewModel
    {
        public List<CreateUserViewModel> Users { get; set; } = new();
        public bool Add { get; set; }

        public bool Edit { get; set; }
        public bool EditPermission { get; set; }

        public bool Delete { get; set; }

    }

}
