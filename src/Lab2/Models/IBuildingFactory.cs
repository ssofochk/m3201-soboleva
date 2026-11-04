namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public interface IBuildingFactory
{
    CommandCenter CreateCommandCenter(int level);

    Extractor CreateExtractor(int workerCount, Material material);

    BasicFactory CreateBasicFactory(Material inputMaterial);

    AdvancedFactory CreateAdvancedFactory(MaterialPair inputMaterials);
}