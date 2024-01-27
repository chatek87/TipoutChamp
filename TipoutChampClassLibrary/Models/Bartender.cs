namespace TipoutChamp;

public class Bartender
{    
    public string Name { get; set; } = "";
    public decimal HoursWorked { get; set; } = 0M;
    public decimal ChargedTips { get; set; } = 0M;
    public decimal Sales { get; set; } = 0M;
    //
    public decimal TipoutToSupport { get; set; } = 0M;
    public decimal TipoutFromServers { get; set; } = 0M;
    public decimal TipoutFromEventServers { get; set; } = 0M;
    public decimal FinalPayout { get; set; } = 0M;
}
