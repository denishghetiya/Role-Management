using System;
using System.Collections.Generic;

namespace RoleManagement.DBContext;

public partial class AllPermission
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string PageId { get; set; } = null!;

    public string AllPermission1 { get; set; } = null!;
}
