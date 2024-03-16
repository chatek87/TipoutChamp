namespace TipoutChamp;

public class CellarEvent
{
    public string Name { get; set; } = "";
    public decimal HoursWorked { get; set; } = 0M;
    public decimal ChargedTips { get; set; } = 0M;
    public decimal Sales { get; set; } = 0M;
    public decimal CashPayments { get; set; } = 0M;
    public decimal TotalAmountTippedOut
    {
        get
        {
            return TipoutToBar + TipoutToSupport;
        }
    }
    public decimal? ToBarOverride { get; set; } = null;
    public decimal? ToSupportOverride { get; set; } = null;

    // all below are calculated by TipoutCalculator()
    public decimal TipoutToBar { get; set; } = 0M;
    public decimal TipoutToSupport { get; set; } = 0M;
    public decimal PercentToBar { get; set; } = 0M;
    public decimal PercentToSupport { get; set; } = 0M;
}
