namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class Planet
{
    public string Name { get; }

    public PlanetType Type { get; }

    public Planet(string name, PlanetType type)
    {
        Name = name;
        Type = type;
    }
}