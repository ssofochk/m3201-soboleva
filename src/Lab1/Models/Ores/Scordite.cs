using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public record Scordite : IOre
{
    public string Name => "Scordite";

    public decimal UnitVolume => 0.15m;

    public IReadOnlyDictionary<IMineral, int> YieldPerPortion => new Dictionary<IMineral, int>
    {
        [new Tritanium()] = 75,
        [new Pyerite()] = 55,
    };
}