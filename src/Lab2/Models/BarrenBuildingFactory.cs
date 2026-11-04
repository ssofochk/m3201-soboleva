namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class BarrenBuildingFactory : IBuildingFactory
{
    public PlanetType Type { get; }

    private readonly RecipeCatalog _recipeCatalog;

    public BarrenBuildingFactory(RecipeCatalog recipeCatalog)
    {
        Dictionary<Material, int> extractedMaterials = new()
        {
            [new Material("Base Metals")] = 1500,
            [new Material("Noble Metals")] = 1500,
            [new Material("Microorganisms")] = 1000,
        };
        Type = new("Barren", extractedMaterials);
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