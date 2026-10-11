using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class ShipAndOccupancy
{
    public ShipAndOccupancy(IShip ship)
    {
        Ship = ship;
        Occupancy = 0;
    }

    public IShip Ship { get; }

    public int Occupancy { get; set; }
}