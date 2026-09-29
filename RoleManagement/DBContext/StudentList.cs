using System;
using System.Collections.Generic;

namespace RoleManagement.DBContext;

public partial class StudentList
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public int RollNumber { get; set; }

    public string Class { get; set; } = null!;
}
