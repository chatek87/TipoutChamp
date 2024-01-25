using System.Linq;

namespace TipoutChamp;

public class TipoutCalculator
{
    public RosterModel Roster { get; set; }

    public decimal TotalBarSales
    {
        get
        {
            return Roster.Employees
                .Where(employee => employee.Role == Roles.Bartender)
                .Sum(employee => employee.Sales);
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


    public TipoutCalculator(RosterModel roster)
    {
        Roster = roster;
    }

    public String GenerateReport()
    {
        return String.Empty;
    }
    

    public void GetBartenderTipout()
    {
        //switch statement:
        //if 1 support - 1% of net sales to support
        //if 1 support - 2% of net sales to support
        //if 3 support - 3% of net sales to support
    }
    public void GetBartenderPay()
    { 
        //totalEarned = totalBarCashTips + totalBarChargedTips - totalBarTipout
        //totalEarned * individual HoursWorked / totalHoursWorked = amount owed to bartender
    }

    public void GetServerTipout()   //this should probably return 
    {
        //for each server
        //net sales * 1%(number of support)
    }

    public void GetServerPay()
    {
        //for each server
        //"tips/grats paid out AKA ChargedTips" - serverTipout - net Cash
    }

    public void GetSupportTipout()
    {
        //totalSupportTipout = barTipout + serverTipout
    }

}
