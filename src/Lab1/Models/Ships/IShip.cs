namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

public interface IShip
{
    string Name { get; }

    int MineOneCycle(int cycleNumber);

    int Hold { get; }

    int Speed { get; }

    int RentalCost { get; }
}