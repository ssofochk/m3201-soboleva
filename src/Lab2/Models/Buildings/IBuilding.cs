namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;

public interface IBuilding
{
    PlanetType Type { get; }

    int Cpu();

    int Pg();

    IBuilding Clone();
}