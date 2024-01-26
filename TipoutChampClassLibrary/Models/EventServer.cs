﻿namespace TipoutChamp;

public class EventServer
{
    public string Name { get; set; }
    public decimal HoursWorked { get; set; } = 0M;
    public decimal ChargedTips { get; set; } = 0M;
    public decimal Sales { get; set; } = 0M;
    public decimal NetCash { get; set; } = 0M;
    public decimal FinalPayout { get; set; } = 0;

}
