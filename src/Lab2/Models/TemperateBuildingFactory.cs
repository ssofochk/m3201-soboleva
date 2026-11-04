namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class TemperateBuildingFactory : IBuildingFactory
{
    public PlanetType Type { get; }

    private readonly RecipeCatalog _recipeCatalog;

    public TemperateBuildingFactory(RecipeCatalog recipeCatalog)
    {
        Dictionary<Material, int> extractedMaterials = new()
        {
            [new Material("Microorganisms")] = 1500,
            [new Material("Autotrophs")] = 1500,
            [new Material("Complex Organisms")] = 1000,
        };
        Type = new("Temperate", extractedMaterials);
        _recipeCatalog = recipeCatalog;
    }

    public CommandCenter CreateCommandCenter(int level)
    {
        return new CommandCenter(Type, level);
    }

    public Extractor CreateExtractor(int workerCount, Material material)
    {
        return new Extractor(Type, workerCount, material);
    }

    public BasicFactory CreateBasicFactory(Material inputMaterial)
    {
        return new BasicFactory(Type, _recipeCatalog, inputMaterial);
    }

    public AdvancedFactory CreateAdvancedFactory(MaterialPair inputMaterials)
    {
        return new AdvancedFactory(Type, _recipeCatalog, inputMaterials);
    }
}