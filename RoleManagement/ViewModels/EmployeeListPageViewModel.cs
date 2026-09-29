namespace RoleManagement.ViewModels
{
    public class EmployeeListPageViewModel
    {
        public List<EmployeeListViewModel> Employees { get; set; } = new();
        public bool Add { get; set; }

        public bool Edit { get; set; }
        public bool EditPermission { get; set; }

        public bool Delete { get; set; }

    }

}
