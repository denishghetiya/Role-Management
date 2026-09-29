using System;
using System.Collections.Generic;

namespace RoleManagement.DBContext;

public partial class PageName
{
    public int PageId { get; set; }

    public string PageName1 { get; set; } = null!;

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
