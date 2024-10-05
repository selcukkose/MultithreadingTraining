using MultithreadingTraining.Cake;
using MultithreadingTraining.RawMaterials;
using MultithreadingTraining.WareHouse;

namespace MultithreadingTraining.CakeMakingRobot.CakePartMaking;

public class CakePartPreparer<T>(IWareHouse wareHouse) where T : CakeMakingPart, new()
{
    public T Prepare()
    {
        Console.WriteLine($"Preparing {typeof(T).Name}...");
        var cakePart = new T();

        foreach (var ingredient in cakePart.Ingredients) AddIngredientFromWareHouse(cakePart, ingredient);

        Console.WriteLine($"Prepared {typeof(T).Name}.");
        return cakePart;
    }

    private void AddIngredientFromWareHouse(CakeMakingPart cakePart, KeyValuePair<string, RawMaterial> ingredient)
    {
        wareHouse.Take(ingredient.Value);
        cakePart.Add(ingredient.Value);
    }
}