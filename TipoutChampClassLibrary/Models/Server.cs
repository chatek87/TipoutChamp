using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipoutChamp;

public class Server : Employee
{
    public decimal ChargedTips { get; set; } = 0M;
    public decimal Sales { get; set; } = 0M;
}
