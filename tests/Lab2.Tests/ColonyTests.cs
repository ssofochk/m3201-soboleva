using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Builder;
using Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;
using Itmo.ObjectOrientedProgramming.Lab2.Models.PlanetBuildingsFactory;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class ColonyTests
{
    [Fact]
    public void BuildColony_WithEnoughResources_ReturnsSuccess()
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory factory = new(catalog);
        Planet planet = new("Barren-I", factory.Type);

        ColonyBuildResult result = BuildMechanicalPartsColony(planet, factory, 2);

        Colony colony = Assert.IsType<Colony>(result.Colony);
        Assert.Null(result.Error);
        Assert.Equal(5, colony.Buildings.Count);
        Assert.Equal(2580, colony.Buildings.Sum(building => building.Cpu()));
        Assert.Equal(11900, colony.Buildings.Sum(building => building.Pg()));
        Assert.Equal(12136, colony.CommandCenter.Cpu());
        Assert.Equal(12000, colony.CommandCenter.Pg());
    }

    [Fact]
    public void BuildColony_WithInsufficientPowerGrid_ReturnsFailure()
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory factory = new(catalog);
        Planet planet = new("Barren-I", factory.Type);

        ColonyBuildResult result = BuildMechanicalPartsColony(planet, factory, 1);

        Assert.Null(result.Colony);
        Assert.NotNull(result.Error);
        Assert.Contains("PG", result.Error, StringComparison.Ordinal);
    }

    [Fact]
    public void BuildColony_WithBuildingFromAnotherPlanetType_ReturnsFailure()
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory barrenFactory = new(catalog);
        LavaBuildingFactory lavaFactory = new(catalog);
        Planet planet = new("Barren-I", barrenFactory.Type);

        ColonyBuildResult result = Colony.Start()
            .WithPlanet(planet)
            .WithCommandCenter(barrenFactory.CreateCommandCenter(2))
            .AddBuilding(lavaFactory.CreateBasicFactory(new Material("Base Metals")))
            .Build();

        Assert.Null(result.Colony);
        Assert.NotNull(result.Error);
        Assert.Contains("Type mismatch", result.Error, StringComparison.Ordinal);
    }

    [Fact]
    public void CreateExtractor_WithUnavailableMaterial_ThrowsArgumentException()
    {
        RecipeCatalog catalog = new();
        LavaBuildingFactory factory = new(catalog);

        Assert.Throws<ArgumentException>(() =>
            factory.CreateExtractor(4, new Material("Noble Metals")));
    }

    [Fact]
    public void CalculateProduction_For24Hours_Produces120MechanicalParts()
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory factory = new(catalog);
        Planet planet = new("Barren-I", factory.Type);

        ColonyBuildResult result = BuildMechanicalPartsColony(planet, factory, 2);
        Colony colony = Assert.IsType<Colony>(result.Colony);

        IReadOnlyDictionary<Material, int> storage = colony.CalculateProduction(24);

        Assert.Equal(120, storage[new Material("Mechanical Parts")]);

        foreach (KeyValuePair<Material, int> entry in storage)
        {
            if (entry.Key != new Material("Mechanical Parts"))
            {
                Assert.Equal(0, entry.Value);
            }
        }
    }

    [Fact]
    public void CloneColony_ToPlanetOfSameType_ReturnsIndependentCopy()
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory factory = new(catalog);
        BarrenBuildingFactory anotherFactory = new(catalog);

        Planet originalPlanet = new("Barren-I", factory.Type);
        Planet targetPlanet = new("Barren-II", anotherFactory.Type);

        ColonyBuildResult buildResult =
            BuildMechanicalPartsColony(originalPlanet, factory, 2);

        Colony original = Assert.IsType<Colony>(buildResult.Colony);

        ColonyCopyResult copyResult = original.Clone(targetPlanet);

        Colony copy = Assert.IsType<Colony>(copyResult.Colony);
        Assert.Null(copyResult.Error);
        Assert.NotSame(original, copy);
        Assert.NotSame(original.CommandCenter, copy.CommandCenter);
        Assert.Same(targetPlanet, copy.Planet);
        Assert.Equal(original.Buildings.Count, copy.Buildings.Count);

        for (int i = 0; i < original.Buildings.Count; i++)
        {
            Assert.NotSame(original.Buildings[i], copy.Buildings[i]);
            Assert.Equal(
                original.Buildings[i].GetType(),
                copy.Buildings[i].GetType());
        }

        IReadOnlyDictionary<Material, int> storage = copy.CalculateProduction(24);
        Assert.Equal(120, storage[new Material("Mechanical Parts")]);
    }

    [Fact]
    public void CloneColony_ToPlanetOfDifferentType_ReturnsFailure()
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory barrenFactory = new(catalog);
        LavaBuildingFactory lavaFactory = new(catalog);

        Planet originalPlanet = new("Barren-I", barrenFactory.Type);
        Planet targetPlanet = new("Lava-I", lavaFactory.Type);

        ColonyBuildResult buildResult =
            BuildMechanicalPartsColony(originalPlanet, barrenFactory, 2);

        Colony original = Assert.IsType<Colony>(buildResult.Colony);

        ColonyCopyResult copyResult = original.Clone(targetPlanet);

        Assert.Null(copyResult.Colony);
        Assert.NotNull(copyResult.Error);
        Assert.Contains("Type mismatch", copyResult.Error, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CalculateProduction_WithInvalidHours_ThrowsArgumentException(int hours)
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory factory = new(catalog);
        Planet planet = new("Barren-I", factory.Type);

        ColonyBuildResult result = BuildMechanicalPartsColony(planet, factory, 2);
        Colony colony = Assert.IsType<Colony>(result.Colony);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            colony.CalculateProduction(hours));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    public void CreateExtractor_WithInvalidWorkerCount_ThrowsArgumentException(int workers)
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory factory = new(catalog);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            factory.CreateExtractor(workers, new Material("Base Metals")));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    public void CreateCommandCenter_WithInvalidLevel_ThrowsArgumentException(int level)
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory factory = new(catalog);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            factory.CreateCommandCenter(level));
    }

    [Fact]
    public void CreateCommandCenter_WithLevelZero_ReturnsCorrectResources()
    {
        RecipeCatalog catalog = new();
        BarrenBuildingFactory factory = new(catalog);

        CommandCenter center = factory.CreateCommandCenter(0);

        Assert.Equal(1675, center.Cpu());
        Assert.Equal(6000, center.Pg());
    }

    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible",
        Justification = "The helper intentionally depends on the abstract building factory.")]
    private static ColonyBuildResult BuildMechanicalPartsColony(
        Planet planet,
        IBuildingFactory factory,
        int commandCenterLevel)
    {
        return Colony.Start()
            .WithPlanet(planet)
            .WithCommandCenter(factory.CreateCommandCenter(commandCenterLevel))
            .AddBuilding(factory.CreateExtractor(4, new Material("Noble Metals")))
            .AddBuilding(factory.CreateExtractor(4, new Material("Base Metals")))
            .AddBuilding(factory.CreateBasicFactory(new Material("Noble Metals")))
            .AddBuilding(factory.CreateBasicFactory(new Material("Base Metals")))
            .AddBuilding(factory.CreateAdvancedFactory(
                new MaterialPair(
                    new Material("Precious Metals"),
                    new Material("Reactive Metals"))))
            .Build();
    }
}