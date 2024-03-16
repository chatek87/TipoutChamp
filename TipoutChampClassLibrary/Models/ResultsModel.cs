using System.ComponentModel;

namespace TipoutChamp;

public class ResultsModel
{
    // this class is instantiated inside TipoutCalculator, and stores the results of calculations
    public BindingList<Server> Servers { get; set; } = new BindingList<Server>();
    public BindingList<Bartender> Bartenders { get; set; } = new BindingList<Bartender>();
    public BindingList<Support> Support { get; set; } = new BindingList<Support>();
    public BindingList<CellarEvent> CellarEvents { get; set; } = new BindingList<CellarEvent>();
}
