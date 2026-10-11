using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public record Veldspar : IOre
{
    public string Name => "Veldspar";

    public decimal UnitVolume => 0.1m;

    public IReadOnlyDictionary<IMineral, int> YieldPerPortion => new Dictionary<IMineral, int>
    {
        [new Tritanium()] = 200,
    };
}