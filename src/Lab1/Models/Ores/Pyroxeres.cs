using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public record Pyroxeres : IOre
{
    public string Name => "Pyroxeres";

    public decimal UnitVolume => 0.3m;

    public IReadOnlyDictionary<IMineral, int> YieldPerPortion => new Dictionary<IMineral, int>
    {
        [new Pyerite()] = 45,
        [new Mexallon()] = 15,
    };
}