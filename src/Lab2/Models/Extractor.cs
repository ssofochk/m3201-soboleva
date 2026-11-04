namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

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

    public Extractor(PlanetType type, int workers,  Material material)
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
}