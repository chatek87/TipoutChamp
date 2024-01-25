namespace TipoutChamp;

public class TipoutCalculator
{
    public RosterModel Roster { get; set; }
    
    public decimal TotalBarSales { get; set; } = 0;
    public TipoutCalculator(RosterModel roster)
    {
        Roster = roster;
        GetBarSales();
    }

    public void GetBarSales()
    {
        TotalBarSales = Roster.Employees
        .Where(employee => employee.Role == Roles.Bartender)
        .Sum(employee => employee.Sales);
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
        //count each bartender
        int bartenderCount = 0;

        foreach (var employee in Roster.Employees)
        {
            if (employee.Role == Roles.Bartender)
            {
                bartenderCount++;
            }
        }

        //totalEarned = totalBarCashTips + totalBarChargedTips + totalBarTipout
        //totalEarned * individual HoursWorked / totalHoursWorked = amount owed to bartender
    }

    public void GetServerTipout()
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
