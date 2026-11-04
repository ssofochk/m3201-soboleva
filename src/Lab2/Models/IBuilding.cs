namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public interface IBuilding
{
    PlanetType Type { get; }

    int Cpu();

    int Pg();
}