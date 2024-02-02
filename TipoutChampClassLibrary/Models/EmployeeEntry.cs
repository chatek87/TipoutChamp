using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipoutChamp;

public class EmployeeEntry : IComparable<EmployeeEntry>
{
    public string Name { get; set; } = "";
    public Roles Role { get; set; }
    public decimal HoursWorked { get; set; } = 0M;
    public decimal ChargedTips { get; set; } = 0M;
    public decimal Sales { get; set; } = 0M;
    public decimal CashPayments { get; set; } = 0M;

    public int CompareTo(EmployeeEntry? other)
    {
        return this.Role.CompareTo(other.Role);
    }
}

