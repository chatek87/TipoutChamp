using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipoutChamp;

public class EmployeeEntry
{
    public string Name { get; set; }
    public Roles Role { get; set; }
    public decimal HoursWorked { get; set; } = 0M;
    public decimal ChargedTips { get; set; } = 0M;
    public decimal Sales { get; set; } = 0M;
    public decimal NetCash { get; set; } = 0M;

}

