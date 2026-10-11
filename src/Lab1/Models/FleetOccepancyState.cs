namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class FleetOccepancyState
{
    public IReadOnlyList<ShipAndOccupancy> Ships { get; }

    public int TotalOccupancy { get; set; }

    public int TotalHold { get; private set; } = 0;

    public FleetOccepancyState(IReadOnlyList<ShipAndOccupancy> ships)
    {
        Ships = ships;
        foreach (ShipAndOccupancy ship in ships)
        {
            TotalHold += ship.Ship.Hold;
        }
    }
}