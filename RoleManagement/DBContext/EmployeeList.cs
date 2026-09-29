using System;
using System.Collections.Generic;

namespace RoleManagement.DBContext;

public partial class EmployeeList
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;
}
