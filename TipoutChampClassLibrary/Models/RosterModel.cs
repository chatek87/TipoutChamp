using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipoutChamp;

namespace TipoutChamp;

public static class RosterModel
{
    public static BindingList<Employee> Employees { get; } = new BindingList<Employee>();
}
