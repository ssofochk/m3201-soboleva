namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

public class Prospect : IShip
{
    public string Name => "Prospect";

    private const int MinningFisrtCycle = 75;
    private const int MinningPerCycle = 150;

    public int MineOneCycle(int cycleNumber)
    {
        if (cycleNumber == 1)
        {
            return MinningFisrtCycle;
        }

        return MinningPerCycle;
    }

    public int Hold => 1000;

    public int Speed => 5;

    public int RentalCost => 1500;
}