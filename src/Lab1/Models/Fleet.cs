using Itmo.ObjectOrientedProgramming.Lab1.Models.FleetPlacementStrategy;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Fleet
{
    public IReadOnlyList<IShip> Ships { get; }

    public int CostPerHour { get; }

    public int Speed { get; }

    private IFleetPlacementStrategy PlacementStrategy { get; }

    public Fleet(
        IReadOnlyList<IShip> ships,
        IFleetPlacementStrategy placementStrategy)
    {
        if (ships.Count == 0)
        {
            throw new ArgumentException("Fleet cannot be empty.");
        }

        Ships = ships;
        PlacementStrategy = placementStrategy;

        int costPerHour = 0;
        int minSpeed = int.MaxValue;

        foreach (IShip ship in Ships)
        {
            costPerHour += ship.RentalCost;
            if (ship.Speed < minSpeed) minSpeed = ship.Speed;
        }

        CostPerHour = costPerHour;
        Speed = minSpeed;
    }

    public bool Place(
        ShipAndOccupancy ship,
        int volume,
        FleetOccupancyState state)
    {
        return PlacementStrategy.Place(ship, volume, state);
    }
}