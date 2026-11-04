using Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Builder;

public interface IBuildingStep
{
    IBuildingStep AddBuilding(IBuilding building);

    ColonyBuildResult Build();
}