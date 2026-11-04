namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class AdvancedFactory : Factory
{
    private readonly int _inputCount = 40;
    private readonly int _outputCount = 5;
    private readonly MaterialPair _recipeInput;

    internal AdvancedFactory(
        PlanetType type,
        RecipeCatalog recipeCatalog,
        MaterialPair recipeInput)
        : base(type, recipeCatalog, 500, 700)
    {
        if (!recipeCatalog.AdvancedRecipes.ContainsKey(recipeInput))
        {
            throw new ArgumentException("No suitable recipe");
        }

        _recipeInput = recipeInput;
    }

    public override void Processing(IDictionary<Material, int> storage)
    {
        Material output = RecipeCatalog.AdvancedRecipes[_recipeInput];
        if (storage.ContainsKey(_recipeInput.First) && storage.ContainsKey(_recipeInput.Second))
        {
            if (storage[_recipeInput.First] >= _inputCount &&
                storage[_recipeInput.Second] >= _inputCount)
            {
                storage[_recipeInput.First] -= _inputCount;
                storage[_recipeInput.Second] -= _inputCount;
                AddToStorage(storage, output, _outputCount);
            }
        }
    }
}