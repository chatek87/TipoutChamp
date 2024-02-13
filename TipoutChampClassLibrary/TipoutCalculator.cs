using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TipoutChamp;

public class TipoutCalculator
{
    private string? _reportString;
    public ResultsModel Roster { get; set; } = new ResultsModel();
    public string ReportString
    {
        get
        {
            if (string.IsNullOrEmpty(_reportString))
            {
                GenerateReportString();
            }
            return _reportString;
        }
    }
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
            if (Roster.Bartenders == null) return 0.00M; 
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
    public decimal TotalSupportTipout
    {
        get
        {
            return Roster.Support.Sum(emp => emp.FinalPayout);
        }
    }
    public decimal CellarFactor { get; set; } = 0.5M;
    public decimal TotalCellarTipoutToBar
    {
        get
        {
            var totalCellarTipoutToBar = 0M;
            foreach (var cellar in Roster.CellarEvents)
            {
                // check for override
                if (cellar.ToBarOverride != null)
                {
                    totalCellarTipoutToBar += cellar.Sales * (decimal)cellar.ToBarOverride / 100;
                }
                else
                {
                    totalCellarTipoutToBar += cellar.Sales * BarTipoutPercentage * CellarFactor;
                }
            }
            return totalCellarTipoutToBar;
        }
    }
    public decimal TotalCellarTipoutToSupport
    {
        get
        {
            var totalCellarTipoutToSupport = 0M;
            foreach (var cellar in Roster.CellarEvents)
            {
                // check for override
                if (cellar.ToSupportOverride != null)
                {
                    totalCellarTipoutToSupport += cellar.Sales * (decimal)cellar.ToSupportOverride / 100;
                }
                else
                {
                    totalCellarTipoutToSupport += cellar.Sales * SupportTipoutPercentage * CellarFactor;
                }
            }
            return totalCellarTipoutToSupport;
        }
    }

    public TipoutCalculator(InputModel input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input), "InputModel cannot be null.");
        }

        LoadInputModelIntoRoster(input);
        RunCalculationsPopulateFields();
        GenerateReportString();
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
                        CashPayments = employee.CashPayments
                    });
                    break;
                case Roles.Support:
                    Roster.Support.Add(new Support
                    {
                        Name = employee.Name,
                        HoursWorked = employee.HoursWorked,

                    });
                    break;
                case Roles.CellarEvent:
                    Roster.CellarEvents.Add(new CellarEvent
                    {
                        Name = employee.Name,
                        Sales = employee.Sales,
                        ToBarOverride = employee.ToBar ?? default(decimal?),
                        ToSupportOverride = employee.ToSupport ?? default(decimal?)
                    });
                    break;
            }
        };
    }
    private void RunCalculationsPopulateFields()
    {
        //bar
        if (TotalBarHours != 0)
        {
            foreach (var emp in Roster.Bartenders)
            {
                emp.TipSharePercentage = emp.HoursWorked / TotalBarHours;
                emp.ShareOfChargedBarTips = emp.TipSharePercentage * TotalBarChargedTips;
                emp.TipoutToSupport = emp.TipSharePercentage * TotalBarSales * SupportTipoutPercentage;
                emp.TipoutFromServers = emp.TipSharePercentage * TotalServerSales * BarTipoutPercentage;
                emp.TipoutFromCellarEvents = TotalCellarTipoutToBar * emp.TipSharePercentage;
                emp.FinalPayout = emp.ShareOfChargedBarTips + emp.TipoutFromServers + emp.TipoutFromCellarEvents;
            }
        }
        
        //server
        foreach (var emp in Roster.Servers)
        {
            emp.TipoutToBar = emp.Sales * BarTipoutPercentage;
            emp.TipoutToSupport = emp.Sales * SupportTipoutPercentage;
            emp.FinalPayout = emp.ChargedTips - emp.TipoutToBar - emp.TipoutToSupport - emp.CashPayments;
        }
        //support
        if (TotalSupportHours != 0)
        {
            foreach (var emp in Roster.Support)
            {
                emp.TipSharePercentage = emp.HoursWorked / TotalSupportHours;
                emp.TipoutFromBar = emp.TipSharePercentage * TotalBarSales * SupportTipoutPercentage;
                emp.TipoutFromServers = emp.TipSharePercentage * TotalServerSales * SupportTipoutPercentage;
                emp.TipoutFromCellarEvents = TotalCellarTipoutToSupport * emp.TipSharePercentage;
                emp.FinalPayout = emp.TipoutFromBar + emp.TipoutFromServers + emp.TipoutFromCellarEvents;
            }
        }
        //cellarEvent
        foreach (var emp in Roster.CellarEvents)
        {
            //if emp.overrides are null
            if (emp.ToBarOverride == null)
            {
                emp.TipoutToBar = emp.Sales * BarTipoutPercentage * CellarFactor;
            }
            else
            {
                emp.TipoutToBar += emp.Sales * (decimal)emp.ToBarOverride / 100;
            }
            
            if (emp.ToSupportOverride == null)
            {
                emp.TipoutToSupport = emp.Sales * SupportTipoutPercentage * CellarFactor;
            }
            else
            {
                emp.TipoutToSupport += emp.Sales * (decimal)emp.ToSupportOverride / 100;
            }
        }
    }
    private void GenerateReportString()
    {
        string spacer = "";
        
        StringBuilder reportBuilder = new StringBuilder();

        reportBuilder.AppendLine($"Total Bar Sales: ${TotalBarSales.ToString("0.00")}");
        reportBuilder.AppendLine($"Total Server Sales: ${TotalServerSales.ToString("0.00")}");
        reportBuilder.AppendLine($"Total Cellar Sales: ${TotalCellarEventSales.ToString("0.00")}");
        reportBuilder.AppendLine(spacer);
        reportBuilder.AppendLine($"% of Sales Tipped Out To Bar: {(BarTipoutPercentage * 100).ToString("0.00")}");
        reportBuilder.AppendLine($"% of Sales Tipped Out To Support: {(SupportTipoutPercentage * 100).ToString("0.00")}");
        reportBuilder.AppendLine(spacer);
        reportBuilder.AppendLine("--BAR--");
        reportBuilder.AppendLine($"Total Bar Hours: {TotalBarHours.ToString("0.00")}");
        reportBuilder.AppendLine($"Total Bar Charged Tips: ${TotalBarChargedTips.ToString("0.00")}");
        reportBuilder.AppendLine(spacer);
        foreach (var emp in Roster.Bartenders)
        {
            reportBuilder.AppendLine($"{emp.Name}   -   Bartender");
            reportBuilder.AppendLine($"Hours Worked: {emp.HoursWorked.ToString("0.00")}");
            reportBuilder.AppendLine($"Charged Tips: ${emp.ChargedTips.ToString("0.00")}");
            reportBuilder.AppendLine($"Sales: ${emp.Sales.ToString("0.00")}");
            reportBuilder.AppendLine($"Share Of Charged Tips: ${emp.ShareOfChargedBarTips.ToString("0.00")} ({(emp.TipSharePercentage * 100).ToString("0.00")}% of Total)");
            reportBuilder.AppendLine($"Tipout To Support: ${emp.TipoutToSupport.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout From Servers: ${emp.TipoutFromServers.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout From Cellar: ${emp.TipoutFromCellarEvents.ToString("0.00")}");
            reportBuilder.AppendLine($"Final Payout: ${emp.FinalPayout.ToString("0.00")}");
            reportBuilder.AppendLine(spacer);
        }
        reportBuilder.AppendLine("--SERVERS--");
        foreach (var emp in Roster.Servers)
        {
            reportBuilder.AppendLine($"{emp.Name}   -   Server");
            reportBuilder.AppendLine($"Charged Tips: ${emp.ChargedTips.ToString("0.00")}");
            reportBuilder.AppendLine($"Net CashPayments: ${emp.CashPayments.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout To Bar: ${emp.TipoutToBar.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout To Support: ${emp.TipoutToSupport.ToString("0.00")}");
            reportBuilder.AppendLine($"Final Payout: ${emp.FinalPayout.ToString("0.00")}");
            reportBuilder.AppendLine(spacer);
        }
        reportBuilder.AppendLine("--SUPPORT--");
        reportBuilder.AppendLine($"Total Support Hours: {TotalSupportHours.ToString("0.00")}");
        reportBuilder.AppendLine($"Total Tipout To Support: ${TotalSupportTipout.ToString("0.00")}");
        reportBuilder.AppendLine(spacer);
        foreach (var emp in Roster.Support)
        {
            reportBuilder.AppendLine($"{emp.Name}   -   Support");
            reportBuilder.AppendLine($"Hours Worked: {emp.HoursWorked.ToString("0.00")}");
            reportBuilder.AppendLine($"Share Of Support Tipout: {(emp.TipSharePercentage * 100).ToString("0.00")}%");
            reportBuilder.AppendLine($"Tipout From Bar: ${emp.TipoutFromBar.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout From Servers: ${emp.TipoutFromServers.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout From Cellar: ${emp.TipoutFromCellarEvents.ToString("0.00")}");
            reportBuilder.AppendLine($"Final Payout: ${emp.FinalPayout.ToString("0.00")}");
            reportBuilder.AppendLine(spacer);
        }
        reportBuilder.AppendLine("--CELLAR EVENTS--");
        foreach (var emp in Roster.CellarEvents)
        {
            reportBuilder.AppendLine($"{emp.Name}   -   Cellar Event");
            reportBuilder.AppendLine($"Sales: ${emp.Sales}");
            reportBuilder.AppendLine($"Tipout To Bar: ${emp.TipoutToBar.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout To Support: ${emp.TipoutToSupport.ToString("0.00")}");
        }

        _reportString = reportBuilder.ToString();
    }

    public string WriteReportToTextFileReturnFileName(string reportString)
    {
        string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
        string exePath = AppDomain.CurrentDomain.BaseDirectory;

        // Define the directory for TipoutReports
        string reportsDirectory = Path.Combine(exePath, "TipoutReports");

        // Check if the directory exists, if not, create it
        if (!Directory.Exists(reportsDirectory))
        {
            Directory.CreateDirectory(reportsDirectory);
        }

        string fileName = $"TipoutReport_{dateTimeNow}.txt";
        // Update the filePath to include the TipoutReports directory
        string filePath = Path.Combine(reportsDirectory, fileName);
        string fileHeader = $"Tipout Report for {dateTimeNow}\n";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(fileHeader);
            writer.WriteLine(reportString);
        }

        FileInfo fileInfo = new FileInfo(filePath);
        fileInfo.IsReadOnly = true;

        return fileName;
    }

}




