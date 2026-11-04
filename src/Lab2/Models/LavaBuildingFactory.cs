namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class LavaBuildingFactory : IBuildingFactory
{
    public PlanetType Type { get; }

    private readonly RecipeCatalog _recipeCatalog;

    public LavaBuildingFactory(RecipeCatalog recipeCatalog)
    {
        Dictionary<Material, int> extractedMaterials = new()
        {
            [new Material("Base Metals")] = 1000,
            [new Material("Heavy Metals")] = 1500,
            [new Material("Felsic Magma")] = 1500,
        };
        Type = new("Lava", extractedMaterials);
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