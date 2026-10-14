using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public interface IOre
{
    string Name { get; }

    decimal UnitVolume { get; }

    IReadOnlyDictionary<IMineral, int> YieldPerPortion { get; }
}