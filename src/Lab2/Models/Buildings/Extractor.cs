namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;

public class Extractor : IBuilding
{
    private const int BaseCpu = 400;
    private const int CpuPerWorker = 110;
    private const int BasePg = 2600;
    private const int PgPerWorker = 550;

    public int Cpu()
    {
        return BaseCpu + (CpuPerWorker * _workersCount);
    }

    public int Pg()
    {
        return BasePg + (PgPerWorker * _workersCount);
    }

    public PlanetType Type { get; }

    public Material Material { get; }

    private readonly int _workersCount;

    internal Extractor(PlanetType type, int workers,  Material material)
    {
        if (workers is < 1 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(workers));
        }

        if (!type.ExtractableMaterials.ContainsKey(material))
        {
            throw new ArgumentException("The material is not extracted on this planet");
        }

        Type = type;
        _workersCount = workers;
        Material = material;
    }

    public void Processing(IDictionary<Material, int> storage)
    {
        AddToStorage(storage, Material, _workersCount * Type.ExtractableMaterials[Material]);
    }

    public Extractor Clone()
    {
        return new Extractor(Type, _workersCount, Material);
    }

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