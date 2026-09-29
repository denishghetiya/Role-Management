using System;
using System.Collections.Generic;

namespace RoleManagement.DBContext;

public partial class CustomerList
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int Amount { get; set; }
}
