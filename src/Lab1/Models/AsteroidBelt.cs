using Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class AsteroidBelt
{
    public AsteroidBelt(string name, decimal distance, IOre ore)
    {
        Name = name;
        Distance = distance;
        Ore = ore;
    }

    public string Name { get; }

    public decimal Distance { get; }

    public IOre Ore { get; }
}