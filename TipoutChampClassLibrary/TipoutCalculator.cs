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
            return Roster.Bartenders.Sum(bartender => bartender.Sales);
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
    public decimal BarTipoutPercentage  // percentage of sales tipped to bar, expressed as a decimal
    {
        get
        {
            if (Roster.Support == null) return 0M; 
            return Roster.Support.Count >= 3 ? 0.015M : 0.02M;
        }
    }
    public decimal SupportTipoutPercentage
    {
        get
        {
            if (Roster.Support == null) return 0M;
            var count = Roster.Support.Count;
            return count <= 3 ? count*.01M : .03M;
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
        RunCalculationsPopulateFields();

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
        foreach (var emp in Roster.Bartenders)
        {
            emp.TipSharePercentage = emp.HoursWorked / TotalBarHours;
            emp.ShareOfChargedBarTips = emp.TipSharePercentage * TotalBarChargedTips;
            emp.TipoutToSupport = emp.TipSharePercentage * TotalBarSales * SupportTipoutPercentage;
            emp.TipoutFromServers = emp.TipSharePercentage * TotalServerSales * BarTipoutPercentage;
            emp.TipoutFromCellarEvents = emp.TipSharePercentage * TotalCellarEventSales * BarTipoutPercentage * CellarFactor;
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
            emp.TipoutFromCellarEvents = emp.TipSharePercentage * TotalCellarEventSales * SupportTipoutPercentage;
            emp.FinalPayout = emp.TipoutFromBar + emp.TipoutFromServers + emp.TipoutFromCellarEvents;
        }
        //cellarEvent
        foreach (var emp in Roster.CellarEvents)
        {
            emp.TipoutToBar = emp.Sales * BarTipoutPercentage * CellarFactor;
            emp.TipoutToSupport = emp.Sales * SupportTipoutPercentage * CellarFactor;
        }
    }
}




