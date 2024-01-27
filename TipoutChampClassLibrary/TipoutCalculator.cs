namespace TipoutChamp;

public class TipoutCalculator
{
    public ResultsModel Roster { get; set; } = new ResultsModel();
    
    public decimal TotalBarHours
    {
        get
        {
            return Roster.Bartenders.Sum(bartender => bartender.HoursWorked);
        }
    }
    public decimal TotalBarChargedTips
    {
        get
        {
            return Roster.Bartenders.Sum(bartender => bartender.ChargedTips);
        }
    }
    public decimal TotalBarSales
    {
        get
        {
            return Roster.Bartenders.Sum(bartender => bartender.HoursWorked);
        }
    }
    public TipoutCalculator(InputModel input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input), "InputModel cannot be null.");
        }

        // Load inputModel into roster
        foreach (var employee in input.Employees)
        {
            if (employee.Role == Roles.Bartender)
            {
                var emp = new Bartender()
                {
                    Name = employee.Name,
                    HoursWorked = employee.HoursWorked,
                    ChargedTips = employee.ChargedTips,
                    Sales = employee.Sales
                };

                Roster.Bartenders.Add(emp);
            }
            else if (employee.Role == Roles.Server)
            {
                var emp = new Server()
                {
                    Name = employee.Name,
                    ChargedTips = employee.ChargedTips,
                    Sales = employee.Sales,
                    NetCash = employee.NetCash
                };

                Roster.Servers.Add(emp);
            }
            else if (employee.Role == Roles.Support)
            {
                var emp = new Support()
                {
                    Name = employee.Name,
                    HoursWorked = employee.HoursWorked
                };

                Roster.Support.Add(emp);
            }
            else if (employee.Role == Roles.CellarEvent)
            {
                var emp = new CellarEvent()
                {
                    Name = employee.Name,
                    Sales = employee.Sales                    
                };

                Roster.EventServers.Add(emp);
            }
        }

        //CalculateFinalPayouts();  //or similar
    }
}


    //public decimal TotalBarTipout
    //{
    //    get
    //    {
    //        return TotalBarSales * NumberOfSupport * .01M;
    //    }
    //}

    //public decimal TotalServerSales
    //{
    //    get
    //    {
    //        return Roster.Employees
    //            .Where(employee => employee.Role == Roles.Server)
    //            .Sum(employee => employee.Sales);
    //    }
    //}

    //public decimal TotalServerTipout
    //{
    //    get
    //    {
    //        return (decimal)NumberOfSupport * .01M * TotalServerSales;
    //    }
    //}

    //public decimal TotalSupportTipout
    //{
    //    get
    //    {
    //        return (TotalBarTipout + TotalServerTipout);
    //    }
    //}

    //public decimal TotalBarSales
    //{
    //    get
    //    {
    //        return Roster.Employees
    //            .Where(employee => employee.Role == Roles.Bartender)
    //            .Sum(employee => employee.Sales);
    //    }
    //}

    //public decimal TotalBarChargedTips
    //{
    //    get
    //    {
    //        return Roster.Employees
    //            .Where(employee => employee.Role == Roles.Bartender)
    //            .Sum(employee => employee.ChargedTips);
    //    }
    //}

    //public decimal TotalBarHours
    //{
    //    get
    //    {
    //        return Roster.Employees
    //            .Where(employee => employee.Role == Roles.Bartender)
    //            .Sum(employee => employee.HoursWorked);
    //    }
    //}

    //public decimal TotalSupportHours
    //{
    //    get
    //    {
    //        return Roster.Employees
    //            .Where(employee => employee.Role == Roles.Support)
    //            .Sum(employee => employee.HoursWorked);
    //    }
    //}

    //public int NumberOfSupport
    //{
    //    get
    //    {
    //        return Roster.Employees.Count(employee => employee.Role == Roles.Support);
    //    }
    //}

    //public int NumberOfServers
    //{
    //    get
    //    {
    //        return Roster.Employees.Count(employee => employee.Role == Roles.Server);
    //    }
    //}

    //public int NumberOfBartenders
    //{
    //    get
    //    {
    //        return Roster.Employees.Count(employee => employee.Role == Roles.Bartender);
    //    }
    //}



    //public String GenerateReport()
    //{
    //    string report =







    //    return report;
    //}

    //public decimal GetBartenderTipoutToSupport(EmployeeEntry employee)
    //{
    //    if (employee.Role != Roles.Bartender)
    //    {
    //        return 0M;
    //    }

    //    decimal bartenderShareOfTipsFactor = employee.HoursWorked / TotalBarHours;

    //    return bartenderShareOfTipsFactor * TotalBarSales * NumberOfSupport * .01M;
    //}

    //public decimal GetBartenderPayReceived(EmployeeEntry employee)
    //{
    //    if (employee.Role != Roles.Bartender)
    //    {
    //        return 0M;
    //    }

    //    var tipout = GetBartenderTipoutToSupport(employee);
    //    decimal bartenderShareOfTipsFactor = employee.HoursWorked / TotalBarHours;

    //    return (bartenderShareOfTipsFactor * TotalBarChargedTips);
    //}

    //public decimal GetServerTipoutToSupport(EmployeeEntry employee)
    //{
    //    if (employee.Role != Roles.Server)
    //    {
    //        return 0M;

    //    }

    //    return employee.Sales * NumberOfSupport * 0.1M;
    //}

    //public decimal GetServerPayReceived(EmployeeEntry employee)
    //{
    //    if (employee.Role != Roles.Server)
    //    {
    //        return 0M;
    //    }

    //    var tipout = GetServerTipoutToSupport(employee);

    //    return employee.ChargedTips - tipout - employee.NetCash;
    //    //if this number is negative, server owes drawer that amount

    //}

    //public decimal GetSupportTipoutReceived(EmployeeEntry employee)
    //{
    //    if (employee.Role != Roles.Support)
    //    {
    //        return 0M;
    //    }

    //    return TotalSupportTipout * (employee.HoursWorked / TotalSupportHours);
    //}

