namespace Itmo.ObjectOrientedProgramming.Lab1.Models.FleetPlacementStrategy;

public record SharedHoldStrategy : IFleetPlacementStrategy
{
    public bool Place(
        ShipAndOccupancy ship,
        int volume,
        FleetOccupancyState occupancyState)
    {
        if (!TryPlace(volume, occupancyState)) return false;
        occupancyState.TotalOccupancy += volume;
        int volumeTaken = Math.Min(ship.Ship.Hold - ship.Occupancy,  volume);
        ship.Occupancy += volumeTaken;
        volume -= volumeTaken;
        if (volume <= 0) return true;

        foreach (ShipAndOccupancy anotherShip in occupancyState.Ships)
        {
            if (anotherShip == ship) continue;
            volumeTaken = Math.Min(anotherShip.Ship.Hold - anotherShip.Occupancy,  volume);
            anotherShip.Occupancy += volumeTaken;
            volume -= volumeTaken;
            if (volume <= 0) return true;
        }

        return false;
    }

    private bool TryPlace(
        int volume,
        FleetOccupancyState occupancyState)
    {
        return occupancyState.TotalHold - occupancyState.TotalOccupancy >= volume;
    }
}