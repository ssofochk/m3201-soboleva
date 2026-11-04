using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Contracts;

public abstract class Contract
{
    public Fleet Fleet { get; }

    public IReadOnlyDictionary<IMineral, decimal> PriceList { get; }

    public AsteroidBelt AsteroidBelt { get; }

    protected Contract(
        AsteroidBelt asteroidBelt,
        Fleet fleet,
        IReadOnlyDictionary<IMineral, decimal> priceList)
    {
        AsteroidBelt = asteroidBelt;
        Fleet = fleet;
        PriceList = priceList;
    }

    public abstract bool EnoughTimeForFirstTrip(decimal flightTime);

    public abstract bool CanStartTrip(
        decimal currentTime,
        decimal flightTime,
        decimal minedVolume);

    public abstract bool CanStartCycle(
        decimal currentTime,
        decimal flightTime,
        decimal minedVolume);
}