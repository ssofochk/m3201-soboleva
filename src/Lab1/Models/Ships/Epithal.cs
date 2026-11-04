namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

public class Epithal : IShip
{
    public string Name => "Epithal";

    private const int MinningPerCycle = 0;

    public int MineOneCycle(int cycleNumber)
    {
        return MinningPerCycle;
    }

    public int Hold => 3000;

    public int Speed => 2;

    public int RentalCost => 500;
}