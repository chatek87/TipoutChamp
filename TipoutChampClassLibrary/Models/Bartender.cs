using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipoutChamp;

namespace TipoutChampClassLibrary;

public class Bartender : Employee
{
    public decimal HoursWorked { get; set; } = 0M;
    public decimal ChargedTips { get; set; } = 0M;
}
