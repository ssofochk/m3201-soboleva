namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Builder;

public interface IPlanetStep
{
    ICommandCenterStep WithPlanet(Planet planet);
}