namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class RecipeCatalog
{
    public IReadOnlyDictionary<Material, Material> BasicRecipes { get; }

    public IReadOnlyDictionary<MaterialPair, Material> AdvancedRecipes { get; }

    public RecipeCatalog()
    {
        BasicRecipes = new Dictionary<Material, Material>
        {
            [new Material("Base Metals")] = new Material("Reactive Metals"),
            [new Material("Noble Metals")] = new Material("Precious Metals"),
            [new Material("Microorganisms")] = new Material("Bacteria"),
            [new Material("Autotrophs")] = new Material("Industrial Fibers"),
            [new Material("Complex Organisms")] = new Material("Proteins"),
            [new Material("Heavy Metals")] = new Material("Toxic Metals"),
            [new Material("Felsic Magma")] = new Material("Silicon"),
        };

        AdvancedRecipes = new Dictionary<MaterialPair, Material>
        {
            [new MaterialPair(
                new Material("Precious Metals"),
                new Material("Reactive Metals"))] = new Material("Mechanical Parts"),

            [new MaterialPair(
                new Material("Reactive Metals"),
                new Material("Bacteria"))] = new Material("Nanites"),

            [new MaterialPair(
                new Material("Proteins"),
                new Material("Bacteria"))] = new Material("Fertilizer"),

            [new MaterialPair(
                new Material("Toxic Metals"),
                new Material("Reactive Metals"))] = new Material("Construction Blocks"),

            [new MaterialPair(
                new Material("Silicon"),
                new Material("Industrial Fibers"))] = new Material("Microfiber Shielding"),
        };
    }
}