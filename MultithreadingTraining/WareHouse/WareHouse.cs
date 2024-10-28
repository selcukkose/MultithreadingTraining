using MultithreadingTraining.Common.Exceptions;
using MultithreadingTraining.Common.Helpers;
using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.WareHouse;

public class WareHouse(Dictionary<string, RawMaterial> items) : IWareHouse
{
    public Dictionary<string, RawMaterial> Items { get; set; } = items;
    private readonly object _setQuantityLockObject = new();

    public async Task<double> AddAsync(RawMaterial rawMaterial)
    {
        var newQuantity = await SetQuantityAsync(rawMaterial, Operation.Plus);
        Console.WriteLine($"Warehouse add {rawMaterial.GetType().Name}: {rawMaterial.Quantity}");
        return newQuantity;
    }

    public async Task<double> TakeAsync(RawMaterial rawMaterial)
    {
        var newQuantityTask = await SetQuantityAsync(rawMaterial, Operation.Minus);
        Console.WriteLine($"Warehouse take {rawMaterial.GetType().Name}: {rawMaterial.Quantity}");
        return newQuantityTask;
    }

    private Task<double> CalculateQuantityAsync(RawMaterial rawMaterial)
    {
        Thread.Sleep(RandomTimelapseGenerator.Generate());
        var item = Items[rawMaterial.GetType().Name];
        if (item == null)
            throw new InvalidOperationException("Item not found");
        return Task.FromResult(item.Quantity);
    }

    private Task<double> SetQuantityAsync(RawMaterial rawMaterial, Operation operation)
    {
        lock (_setQuantityLockObject)
        {
            // await can not be used in lock block
            var currentQuantity = CalculateQuantityAsync(rawMaterial).Result;
            Thread.Sleep(RandomTimelapseGenerator.Generate());

            switch (operation)
            {
                case Operation.Plus:
                    currentQuantity += rawMaterial.Quantity;
                    break;
                case Operation.Minus:
                    if (currentQuantity < rawMaterial.Quantity)
                    {
                        var requiredQuantity = rawMaterial.Quantity - currentQuantity;
                        throw new NotEnoughRawMaterialException(rawMaterial.GetType().Name, requiredQuantity);
                    }
                    currentQuantity -= rawMaterial.Quantity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
            var item = Items[rawMaterial.GetType().Name];
            item.Quantity = currentQuantity;
            return Task.FromResult(currentQuantity);
        }
       
    }

    private enum Operation
    {
        Plus,
        Minus
    }
}