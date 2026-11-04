namespace Itmo.ObjectOrientedProgramming.Lab2.Models.Buildings;

public class BasicFactory : Factory
{
    private readonly int _inputCount = 6000;
    private readonly int _outputCount = 40;
    private readonly Material _recipeInput;

    internal BasicFactory(
        PlanetType type,
        RecipeCatalog recipeCatalog,
        Material recipeInput)
        : base(type, recipeCatalog, 200, 800)
    {
        if (!recipeCatalog.BasicRecipes.ContainsKey(recipeInput))
        {
            throw new ArgumentException("No suitable recipe");
        }

        _recipeInput = recipeInput;
    }

    public override BasicFactory Clone()
    {
        return new BasicFactory(Type, RecipeCatalog,  _recipeInput);
    }

    public override void Processing(IDictionary<Material, int> storage)
    {
        Material output = RecipeCatalog.BasicRecipes[_recipeInput];
        if (storage.ContainsKey(_recipeInput))
        {
            if (storage[_recipeInput] >= _inputCount)
            {
                storage[_recipeInput] -= _inputCount;
                AddToStorage(storage, output, _outputCount);
            }
        }
    }
}