using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipoutChamp;

namespace TipoutChamp;

public class RosterModel
{
    public BindingList<Employee> Employees { get; } = new BindingList<Employee>();
}
