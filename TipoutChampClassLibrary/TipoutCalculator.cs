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
    public decimal TotalSupportHours
    {
        get
        {
            return Roster.Support.Sum(support => support.HoursWorked);
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
    public decimal TotalServerSales
    {
        get
        {
            return Roster.Servers.Sum(server => server.Sales);
        }
    }
    public decimal TotalCellarEventSales
    {
        get
        {
            return Roster.CellarEvents.Sum(cellar => cellar.Sales);
        }
    }
    public decimal BarTipoutPercentage  // factor by which TotalServerSales is multiplied to get total tipout from servers to bar
    {
        get
        {
            if (Roster.Support == null) return 1M; 
            return Roster.Support.Count >= 3 ? 0.015M : 0.02M;
        }
    }
    public decimal SupportTipoutPercentage
    {
        get
        {
            if (Roster.Support == null) return 1M;
            var count = Roster.Support.Count;
            return count <= 3 ? count : 3;
        }
    }
    public decimal CellarFactor { get; set; } = 0.5M;
    public TipoutCalculator(InputModel input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input), "InputModel cannot be null.");
        }

        LoadInputModelIntoRoster(input);
        // Load inputModel into roster
       
        //CalculateFinalPayouts();  //or similar
    }

    private void LoadInputModelIntoRoster(InputModel input)
    {
        foreach (var employee in input.Employees)
        {
            switch (employee.Role)
            {
                case Roles.Bartender:
                    Roster.Bartenders.Add(new Bartender
                    {
                        Name = employee.Name,
                        HoursWorked = employee.HoursWorked,
                        ChargedTips = employee.ChargedTips,
                        Sales = employee.Sales
                    });
                    break;
                case Roles.Server:
                    Roster.Servers.Add(new Server
                    {
                        Name = employee.Name,
                        ChargedTips = employee.ChargedTips,
                        Sales = employee.Sales,
                        NetCash = employee.NetCash
                    });
                    break;
                case Roles.Support:
                    Roster.Support.Add(new Support
                    {
                        Name = employee.Name,
                        HoursWorked = employee.HoursWorked
                    });
                    break;
                case Roles.CellarEvent:
                    Roster.CellarEvents.Add(new CellarEvent
                    {
                        Name = employee.Name,
                        Sales = employee.Sales
                    });
                    break;
            }
        };
    }
    private void RunCalculationsPopulateFields()
    {
        //bar
        //tipSharePercentage
        foreach (var emp in Roster.Bartenders)
        {
            emp.TipSharePercentage = emp.HoursWorked / TotalBarHours;
            emp.TipoutToSupport = emp.TipSharePercentage * TotalBarSales * SupportTipoutPercentage;
            emp.TipoutFromServers = emp.TipSharePercentage * TotalServerSales * BarTipoutPercentage;
            emp.TipoutFromCellarEvents = emp.TipSharePercentage * TotalCellarEventSales * BarTipoutPercentage * CellarFactor;
            emp.ShareOfChargedBarTips = emp.TipSharePercentage * TotalBarChargedTips;
            emp.FinalPayout = emp.ShareOfChargedBarTips + emp.TipoutFromServers + emp.TipoutFromCellarEvents;
        }
        //server
        foreach (var emp in Roster.Servers)
        {
            emp.TipoutToBar = emp.Sales * BarTipoutPercentage;
            emp.TipoutToSupport = emp.Sales * SupportTipoutPercentage;
            emp.FinalPayout = emp.ChargedTips - emp.TipoutToBar - emp.TipoutToSupport - emp.NetCash;
            
        }
        //support
        foreach (var emp in Roster.Support)
        {
            emp.TipSharePercentage = emp.HoursWorked / TotalSupportHours;
            emp.TipoutFromBar = emp.TipSharePercentage * TotalBarSales * SupportTipoutPercentage;
            emp.TipoutFromServers = emp.TipSharePercentage * TotalServerSales * SupportTipoutPercentage;
        }

        //cellarEvent
        foreach (var emp in Roster.CellarEvents)
        {
            emp.TipoutToBar = emp.Sales * BarTipoutPercentage * CellarFactor;
            emp.TipoutToSupport = emp.Sales * SupportTipoutPercentage * CellarFactor;
        }
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

