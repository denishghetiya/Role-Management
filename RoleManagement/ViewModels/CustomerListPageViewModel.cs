namespace RoleManagement.ViewModels
{
    public class CustomerListPageViewModel
    {
        public List<CustomerListViewModel> Customers { get; set; } = new();
        public bool Add { get; set; }

        public bool Edit { get; set; }
        public bool EditPermission { get; set; }

        public bool Delete { get; set; }

    }

}
