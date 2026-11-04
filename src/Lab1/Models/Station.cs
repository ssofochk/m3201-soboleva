using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Station
{
    public decimal TaxRate { get; }

    public Station(decimal taxRate)
    {
        if (taxRate < 0 || taxRate > 1)
        {
            throw new ArgumentException("Tax rate must be between 0 and 1.");
        }

        TaxRate = taxRate;
    }

    public ProcessingResult Process(
        IOre ore,
        decimal minedVolume,
        int remainingOreUnits)
    {
        int newOreUnits = (int)(minedVolume / ore.UnitVolume);

        int totalOreUnits = remainingOreUnits + newOreUnits;

        int portions = totalOreUnits / 100;
        int newRemainingOreUnits = totalOreUnits % 100;

        var minerals = new Dictionary<IMineral, int>();

        foreach (KeyValuePair<IMineral, int> mineralYield in ore.YieldPerPortion)
        {
            minerals[mineralYield.Key] = mineralYield.Value * portions;
        }

        return new ProcessingResult(
            newRemainingOreUnits,
            minerals);
    }
}