namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class RecipeCatalog
{
    public IReadOnlyDictionary<Material, Material> BasicRecipes { get; }

    public IReadOnlyDictionary<MaterialPair, Material> AdvancedRecipes { get; }

    public RecipeCatalog(
        IReadOnlyDictionary<Material, Material> basicRecipes,
        IReadOnlyDictionary<MaterialPair, Material> advancedRecipes)
    {
        BasicRecipes = basicRecipes;
        AdvancedRecipes = advancedRecipes;
    }
}