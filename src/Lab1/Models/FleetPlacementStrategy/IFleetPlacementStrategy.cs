namespace Itmo.ObjectOrientedProgramming.Lab1.Models.FleetPlacementStrategy;

public interface IFleetPlacementStrategy
{
    bool Place(
        ShipAndOccupancy ship,
        int volume,
        FleetOccepancyState occupancyState);
}