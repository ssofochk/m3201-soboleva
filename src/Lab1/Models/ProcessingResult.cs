using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public record ProcessingResult(
    int RemainingOreUnits,
    IReadOnlyDictionary<IMineral, int> Minerals);