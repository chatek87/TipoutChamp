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
    private void GenerateReportString()
    {
        string spacer = "";
        
        StringBuilder reportBuilder = new StringBuilder();

        reportBuilder.AppendLine($"Total Bar Sales: ${this.TotalBarSales.ToString("0.00")}");
        reportBuilder.AppendLine($"Total Server Sales: ${this.TotalServerSales.ToString("0.00")}");
        reportBuilder.AppendLine($"Total Cellar Sales: ${this.TotalCellarEventSales.ToString("0.00")}");
        reportBuilder.AppendLine(spacer);
        reportBuilder.AppendLine($"% of Sales Tipped Out To Bar: {(this.BarTipoutPercentage * 100).ToString("0.0")}");
        reportBuilder.AppendLine($"% of Sales Tipped Out To Support: {(this.SupportTipoutPercentage * 100).ToString("0.0")}");
        reportBuilder.AppendLine(spacer);
        reportBuilder.AppendLine(spacer);
        reportBuilder.AppendLine("--BAR--");
        reportBuilder.AppendLine($"Total Bar Hours: {this.TotalBarHours.ToString("0.00")}");
        reportBuilder.AppendLine($"Total Bar Charged Tips: ${this.TotalBarChargedTips.ToString("0.00")}");
        reportBuilder.AppendLine(spacer);
        foreach (var emp in this.Roster.Bartenders)
        {
            reportBuilder.AppendLine($"{emp.Name}   -   Bartender");
            reportBuilder.AppendLine($"Hours Worked: {emp.HoursWorked.ToString("0.00")}");
            reportBuilder.AppendLine($"Charged Tips: ${emp.ChargedTips.ToString("0.00")}");
            reportBuilder.AppendLine($"Sales: ${emp.Sales.ToString("0.00")}");
            reportBuilder.AppendLine($"Share Of Charged Tips: ${emp.ShareOfChargedBarTips.ToString("0.00")} ({(emp.TipSharePercentage * 100).ToString("0.0")}% of Total)");
            reportBuilder.AppendLine($"Tipout To Support: ${emp.TipoutToSupport.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout From Servers: ${emp.TipoutFromServers.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout From Cellar: ${emp.TipoutFromCellarEvents.ToString("0.00")}");
            reportBuilder.AppendLine($"Final Payout: ${emp.FinalPayout.ToString("0.00")}");
            reportBuilder.AppendLine(spacer);
        }
        reportBuilder.AppendLine(spacer);
        reportBuilder.AppendLine("--SERVERS--");
        foreach (var emp in this.Roster.Servers)
        {
            reportBuilder.AppendLine($"{emp.Name}   -   Server");
            reportBuilder.AppendLine($"Charged Tips: ${emp.ChargedTips.ToString("0.00")}");
            reportBuilder.AppendLine($"Net Cash: ${emp.NetCash.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout To Bar: ${emp.TipoutToBar.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout To Support: ${emp.TipoutToSupport.ToString("0.00")}");
            reportBuilder.AppendLine($"Final Payout: ${emp.FinalPayout.ToString("0.00")}");
            reportBuilder.AppendLine(spacer);
        }
        reportBuilder.AppendLine(spacer);
        reportBuilder.AppendLine("--SUPPORT--");
        reportBuilder.AppendLine($"Total Support Hours: {this.TotalSupportHours.ToString("0.00")}");
        reportBuilder.AppendLine(spacer);
        foreach (var emp in this.Roster.Support)
        {
            reportBuilder.AppendLine($"{emp.Name}   -   Support");
            reportBuilder.AppendLine($"Hours Worked: {emp.HoursWorked.ToString("0.00")}");
            reportBuilder.AppendLine($"Share Of Support Tipout: {emp.TipSharePercentage.ToString("0.0")}%");
            reportBuilder.AppendLine($"Tipout From Bar: ${emp.TipoutFromBar.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout From Servers: ${emp.TipoutFromServers.ToString("0.00")}");
            reportBuilder.AppendLine($"Tipout From Cellar: ${emp.TipoutFromCellarEvents.ToString("0.00")}");
            reportBuilder.AppendLine($"Final Payout: ${emp.FinalPayout.ToString("0.00")}");
            reportBuilder.AppendLine(spacer);
        }
        reportBuilder.AppendLine(spacer);
        reportBuilder.AppendLine("--CELLAR EVENTS--");
        foreach (var emp in this.Roster.CellarEvents)
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
       
        string fileName = $"TipoutReport_{dateTimeNow}.txt";
        string filePath = Path.Combine(exePath, fileName);
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




