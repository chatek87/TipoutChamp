using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipoutChamp;

public class EmployeeEntry
{
    public string Name { get; set; } = "";
    public Roles Role { get; set; }
    public decimal HoursWorked { get; set; } = 0M;
    public decimal ChargedTips { get; set; } = 0M;
    public decimal Sales { get; set; } = 0M;
    public decimal CashPayments { get; set; } = 0M;

    private decimal? toBar = null;
    public decimal? ToBar
    {
        get => toBar;
        set
        {
            if (value < 0)
            {
                toBar = 0;
            }
            else if (value > 4.00M)
            {
                toBar = 4.00M; 
            }
            else
            {
                toBar = value;
            }
        }
    }

    private decimal? toSupport = null;
    public decimal? ToSupport
    {
        get => toSupport;
        set
        {
            if (value < 0)
            {
                toSupport = 0;
            }
            else if (value > 4.00M)
            {
                toSupport = 4.00M;
            }
            else
            {
                toSupport = value;
            }
        }
    }
}

