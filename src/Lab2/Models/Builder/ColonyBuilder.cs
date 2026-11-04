using Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Builder;

internal class ColonyBuilder : IPlanetStep, ICommandCenterStep, IBuildingStep
{
    private readonly List<IBuilding> _buildings = new();
    private Planet? _planet;
    private CommandCenter? _commandCenter;

    public ICommandCenterStep WithPlanet(Planet planet)
    {
        _planet = planet;
        return this;
    }

    public IBuildingStep WithCommandCenter(CommandCenter commandCenter)
    {
        _commandCenter = commandCenter;
        return this;
    }

    public IBuildingStep AddBuilding(IBuilding building)
    {
        _buildings.Add(building);
        return this;
    }

    public ColonyBuildResult Build()
    {
        if (_planet is null || _commandCenter is null)
        {
            throw new InvalidOperationException("Builder is not initialized");
        }

        int totalCpu = 0;
        int totalPg = 0;
        if (_planet.Type.Name != _commandCenter.Type.Name)
        {
            return new ColonyBuildResult(null, "Type mismatch: planet and command center");
        }

        PlanetType planetType = _planet.Type;
        foreach (IBuilding building in _buildings)
        {
            totalCpu += building.Cpu();
            totalPg += building.Pg();
            if (planetType != building.Type)
            {
                return new ColonyBuildResult(null, "Type mismatch: planet and building");
            }
        }

        if (totalCpu > _commandCenter.Cpu() || totalPg > _commandCenter.Pg())
        {
            return new ColonyBuildResult(null, "CPU or PG limit exceeded");
        }

        Colony colony = new(_planet, _commandCenter, _buildings);
        return new ColonyBuildResult(colony, null);
    }
}