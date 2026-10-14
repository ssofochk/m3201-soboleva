using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public record Kernite : IOre
{
    public string Name => "Kernite";

    public decimal UnitVolume => 1.2m;

    public IReadOnlyDictionary<IMineral, int> YieldPerPortion => new Dictionary<IMineral, int>
    {
        [new Mexallon()] = 30,
        [new Isogen()] = 60,
    };
}