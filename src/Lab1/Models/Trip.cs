using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Trip
{
    public decimal MinedVolume { get; private set; }

    private Fleet Fleet { get; }

    private FleetOccupancyState FleetOccupancyState { get; }

    public Trip(Fleet fleet)
    {
        Fleet = fleet;
        var shipsWithOccupancy = new List<ShipAndOccupancy>();

        foreach (IShip ship in fleet.Ships)
        {
            shipsWithOccupancy.Add(new ShipAndOccupancy(ship));
        }

        FleetOccupancyState = new FleetOccupancyState(shipsWithOccupancy);
    }

    public int MineCycle(int cycleNumber)
    {
        int minedThisCycle = 0;

        foreach (ShipAndOccupancy ship in FleetOccupancyState.Ships)
        {
            int volume = ship.Ship.MineOneCycle(cycleNumber);

            if (volume == 0)
                continue;

            if (Fleet.Place(ship, volume, FleetOccupancyState))
            {
                minedThisCycle += volume;
            }
        }

        MinedVolume += minedThisCycle;

        return minedThisCycle;
    }
}