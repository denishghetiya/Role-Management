namespace RoleManagement.ViewModels
{
    public class StudentListPageViewModel
    {
        public List<StudentListViewModel> Students { get; set; } = new();
        public bool Add { get; set; }

        public bool Edit { get; set; }
        public bool EditPermission { get; set; }

        public bool Delete { get; set; }

    }

}
