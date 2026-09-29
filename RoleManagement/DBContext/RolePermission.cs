using System;
using System.Collections.Generic;

namespace RoleManagement.DBContext;

public partial class RolePermission
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PageId { get; set; }

    public bool Add { get; set; }

    public bool Edit { get; set; }

    public bool Delete { get; set; }

    public bool View { get; set; }

    public bool EditPermission { get; set; }

    public virtual PageName Page { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
