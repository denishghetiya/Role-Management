using System;
using System.Collections.Generic;

namespace RoleManagement.DBContext;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;
}
