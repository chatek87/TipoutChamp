﻿namespace TipoutChamp;

public class Support
{
    public string Name { get; set; } = "";
    public decimal HoursWorked { get; set; } = 0M;
    //
    public decimal TipoutFromBar { get; set; } = 0M;
    public decimal TipoutFromServers { get; set; } = 0M;
    public decimal TipoutFromEventServers { get; set; } = 0M;
    public decimal FinalPayout { get; set; } = 0M;
}