using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Contracts;

public class TermContract : Contract
{
    public decimal Term { get; }

    public TermContract(
        AsteroidBelt asteroidBelt,
        Fleet fleet,
        IReadOnlyDictionary<IMineral, decimal> priceList,
        decimal term)
        : base(asteroidBelt, fleet, priceList)
    {
        if (term <= 0)
        {
            throw new ArgumentException("Term must be positive.");
        }

        Term = term;
    }

    public override bool EnoughTimeForFirstTrip(decimal flightTime)
    {
        return flightTime + 1 + flightTime <= Term;
    }

    public override bool CanStartTrip(
        decimal currentTime,
        decimal flightTime,
        decimal minedVolume)
    {
        return currentTime + flightTime + 1 + flightTime <= Term;
    }

    public override bool CanStartCycle(
        decimal currentTime,
        decimal flightTime,
        decimal minedVolume)
    {
        return currentTime + 1 + flightTime <= Term;
    }
}