using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Contracts;

public class VolumeContract : Contract
{
    public decimal TargetVolume { get; }

    public VolumeContract(
        AsteroidBelt asteroidBelt,
        Fleet fleet,
        IReadOnlyDictionary<IMineral, decimal> priceList,
        decimal targetVolume)
        : base(asteroidBelt, fleet, priceList)
    {
        if (targetVolume <= 0)
        {
            throw new ArgumentException("Target volume must be positive.");
        }

        TargetVolume = targetVolume;
    }

    public override bool EnoughTimeForFirstTrip(decimal flightTime)
    {
        return true;
    }

    public override bool CanStartTrip(
        decimal currentTime,
        decimal flightTime,
        decimal minedVolume)
    {
        return minedVolume < TargetVolume;
    }

    public override bool CanStartCycle(
        decimal currentTime,
        decimal flightTime,
        decimal minedVolume)
    {
        return minedVolume < TargetVolume;
    }
}