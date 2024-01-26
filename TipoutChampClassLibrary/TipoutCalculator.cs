namespace TipoutChamp;

public class TipoutCalculator
{
    public InputModel Roster { get; set; }

    public decimal TotalBarTipout
    {
        get
        {
            return TotalBarSales * NumberOfSupport * .01M;
        }
    }
    public decimal TotalServerSales
    {
        get
        {
            return Roster.Employees
                .Where(employee => employee.Role == Roles.Server)
                .Sum(employee => employee.Sales);
        }
    }
    public decimal TotalServerTipout
    {
        get
        {
            return (decimal)NumberOfSupport * .01M * TotalServerSales;
        }
    }
    public decimal TotalSupportTipout
    {
        get
        {
            return (TotalBarTipout + TotalServerTipout);
        }
    }
    public decimal TotalBarSales
    {
        get
        {
            return Roster.Employees
                .Where(employee => employee.Role == Roles.Bartender)
                .Sum(employee => employee.Sales);
        }
    }
    public decimal TotalBarChargedTips
    {
        get
        {
            return Roster.Employees
                .Where(employee => employee.Role == Roles.Bartender)
                .Sum(employee => employee.ChargedTips);
        }
    }
    public decimal TotalBarHours 
    {
        get
        {
            return Roster.Employees
                .Where(employee => employee.Role == Roles.Bartender)
                .Sum(employee => employee.HoursWorked);
        }
    }
    public decimal TotalSupportHours
    {
        get
        {
            return Roster.Employees
                .Where(employee => employee.Role == Roles.Support)
                .Sum(employee => employee.HoursWorked);
        }
    }
    public int NumberOfSupport
    {
        get
        {
            return Roster.Employees.Count(employee => employee.Role == Roles.Support);
        }
    }
    public int NumberOfServers
    {
        get
        {
            return Roster.Employees.Count(employee => employee.Role == Roles.Server);
        }
    }
    public int NumberOfBartenders
    {
        get
        {
            return Roster.Employees.Count(employee => employee.Role == Roles.Bartender);
        }
    }

    public TipoutCalculator(InputModel roster)
    {
        Roster = roster;
    }

    public String GenerateReport()
    {
        string report = 





           
        return report;
    }    

    public decimal GetBartenderTipoutToSupport(EmployeeEntry employee)
    {
        if (employee.Role != Roles.Bartender)
        {
            return 0M;
        }

        decimal bartenderShareOfTipsFactor = employee.HoursWorked / TotalBarHours;

        return bartenderShareOfTipsFactor * TotalBarSales * NumberOfSupport * .01M;
    }
    public decimal GetBartenderPayReceived(EmployeeEntry employee)
    { 
        if (employee.Role != Roles.Bartender)
        {
            return 0M;
        }
        
        var tipout = GetBartenderTipoutToSupport(employee);
        decimal bartenderShareOfTipsFactor = employee.HoursWorked / TotalBarHours;

        return (bartenderShareOfTipsFactor * TotalBarChargedTips);
    }

    public decimal GetServerTipoutToSupport(EmployeeEntry employee)
    {
        if (employee.Role != Roles.Server)
        {
            return 0M;

        }

        return employee.Sales * NumberOfSupport * 0.1M;
    }
    public decimal GetServerPayReceived(EmployeeEntry employee)
    {
        if (employee.Role != Roles.Server)
        {
            return 0M;
        }
        
        var tipout = GetServerTipoutToSupport(employee);

        return employee.ChargedTips - tipout - employee.NetCash;
        //if this number is negative, server owes drawer that amount

    }

    public decimal GetSupportTipoutReceived(EmployeeEntry employee)
    {
        if (employee.Role != Roles.Support)
        {
            return 0M;
        }

        return TotalSupportTipout * (employee.HoursWorked / TotalSupportHours);
    }

}
