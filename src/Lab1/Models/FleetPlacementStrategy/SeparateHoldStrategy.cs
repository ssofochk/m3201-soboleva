namespace Itmo.ObjectOrientedProgramming.Lab1.Models.FleetPlacementStrategy;

public record SeparateHoldStrategy : IFleetPlacementStrategy
{
    public bool Place(
        ShipAndOccupancy ship,
        int volume,
        FleetOccupancyState occupancyState)
    {
        if (volume <= ship.Ship.Hold - ship.Occupancy)
        {
            ship.Occupancy += volume;
            return true;
        }

        return false;
    }
}