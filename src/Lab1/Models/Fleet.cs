using Itmo.ObjectOrientedProgramming.Lab1.Models.FleetPlacementStrategy;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Fleet
{
    public IReadOnlyList<ShipAndOccupancy> Ships { get; }

    private IFleetPlacementStrategy PlacementStrategy { get; }

    private FleetOccepancyState FleetOccepancyState { get; }

    public Fleet(IReadOnlyList<ShipAndOccupancy> ships,  IFleetPlacementStrategy placementStrategy)
    {
        Ships = ships;
        PlacementStrategy = placementStrategy;
        FleetOccepancyState = new FleetOccepancyState(ships);
        CalculateCostPerHour();
    }

    public void Mine(int cycleNumber)
    {
        foreach (ShipAndOccupancy shipAndOccupancy in Ships)
        {
            int volume = shipAndOccupancy.Ship.MineOneCycle(cycleNumber);
            PlacementStrategy.Place(shipAndOccupancy, volume, FleetOccepancyState);
        }
    }

    public int CostPerHour { get; private set; } = 0;

    private void CalculateCostPerHour()
    {
        foreach (ShipAndOccupancy shipAndOccupancy in Ships)
        {
            CostPerHour += shipAndOccupancy.Ship.RentalCost;
        }
    }
}