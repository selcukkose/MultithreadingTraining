using MultithreadingTraining.Cake;
using MultithreadingTraining.RawMaterials;
using MultithreadingTraining.WareHouse;

namespace MultithreadingTraining.CakeMakingRobot.CakePartMaking;

public class CakePartPreparer<T>(IWareHouse wareHouse) where T : CakeMakingPart, new()
{
    
    public Task<T> PrepareAsync()
    {
        Console.WriteLine($"Preparing {typeof(T).Name}...");
        var cakePart = new T();
        var addIngredientTaskList = new List<Task>();

        foreach (var ingredient in cakePart.Ingredients) addIngredientTaskList.Add(AddIngredientFromWareHouseAsync(cakePart, ingredient)); 

        Task.WaitAll(addIngredientTaskList.ToArray());
        Console.WriteLine($"Prepared {typeof(T).Name}.");
        return Task.FromResult(cakePart);
    }
    
    private async Task AddIngredientFromWareHouseAsync(CakeMakingPart cakePart, KeyValuePair<string, RawMaterial> ingredient)
    {
        var takeTask = wareHouse.TakeAsync(ingredient.Value);
        var addTask = cakePart.AddAsync(ingredient.Value);
        await Task.WhenAll(takeTask, addTask);
    }
}