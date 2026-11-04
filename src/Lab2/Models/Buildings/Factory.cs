namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;

public abstract class Factory : IBuilding
{
    private readonly int _cpu;
    private readonly int _pg;

    public PlanetType Type { get; }

    protected RecipeCatalog RecipeCatalog { get; }

    public int Cpu() => _cpu;

    public int Pg() => _pg;

    protected Factory(PlanetType planetType, RecipeCatalog recipeCatalog, int cpu, int pg)
    {
        Type = planetType;
        RecipeCatalog = recipeCatalog;
        _cpu = cpu;
        _pg = pg;
    }

    public abstract void Processing(IDictionary<Material, int> storage);

    public abstract IBuilding Clone();

    protected void AddToStorage(
        IDictionary<Material, int> storage,
        Material material,
        int count)
    {
        if (!storage.ContainsKey(material))
        {
            storage.Add(material, count);
        }
        else
        {
            storage[material] += count;
        }
    }
}