namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public record PlanetType
{
    public string Name { get; }

    public IReadOnlyDictionary<Material, int> ExtractableMaterials { get; }

    public PlanetType(string name, IReadOnlyDictionary<Material, int> extractedMaterials)
    {
        Name = name;
        ExtractableMaterials = extractedMaterials;
    }
}