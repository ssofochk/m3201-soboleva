namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

public class Venture : IShip
{
    public string Name => "Venture";

    private const int MinningPerCycle = 100;

    public int MineOneCycle(int cycleNumber)
    {
        return MinningPerCycle;
    }

    public int Hold => 400;

    public int Speed => 4;

    public int RentalCost => 1000;
}