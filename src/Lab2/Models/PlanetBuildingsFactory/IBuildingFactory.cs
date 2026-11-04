using Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.PlanetBuildingsFactory;

public interface IBuildingFactory
{
    CommandCenter CreateCommandCenter(int level);

    Extractor CreateExtractor(int workerCount, Material material);

    BasicFactory CreateBasicFactory(Material inputMaterial);

    AdvancedFactory CreateAdvancedFactory(MaterialPair inputMaterials);
}