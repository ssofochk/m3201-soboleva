using Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Builder;

public class Colony
{
    public Planet Planet { get; }

    public CommandCenter CommandCenter { get; }

    public IReadOnlyList<IBuilding> Buildings { get; }

    internal Colony(Planet planet, CommandCenter commandCenter, List<IBuilding> buildings)
    {
        Planet = planet;
        this.CommandCenter = commandCenter;
        this.Buildings = new List<IBuilding>(buildings).AsReadOnly();
    }

    public static IPlanetStep Start()
    {
        return new ColonyBuilder();
    }

    public IReadOnlyDictionary<Material, int> CalculateProduction(int hours)
    {
        if (hours <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(hours), hours, "Hours must be greater than zero.");
        }

        var storage = new Dictionary<Material, int>();
        for (int h = hours; h > 0; h--)
        {
            foreach (Extractor extractor in Buildings.OfType<Extractor>())
            {
                extractor.Processing(storage);
            }

            foreach (BasicFactory basicFactory in Buildings.OfType<BasicFactory>())
            {
                basicFactory.Processing(storage);
            }

            foreach (AdvancedFactory advancedFactory in Buildings.OfType<AdvancedFactory>())
            {
                advancedFactory.Processing(storage);
            }
        }

        return new Dictionary<Material, int>(storage).AsReadOnly();
    }

    public ColonyCopyResult Clone(Planet newPlanet)
    {
        if (Planet.Type.Name != newPlanet.Type.Name)
        {
            return new ColonyCopyResult(null, "Type mismatch");
        }

        CommandCenter newCommandCenter = CommandCenter.Clone();
        var newBuildings = new List<IBuilding>();
        foreach (IBuilding building in Buildings)
        {
            newBuildings.Add(building.Clone());
        }

        var newColony = new Colony(newPlanet, newCommandCenter, newBuildings);
        return new ColonyCopyResult(newColony, null);
    }
}