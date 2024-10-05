using MultithreadingTraining.Common.Exceptions;
using MultithreadingTraining.Common.Helpers;
using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.WareHouse;

public class WareHouse(Dictionary<string, RawMaterial> items) : IWareHouse
{
    public Dictionary<string, RawMaterial> Items { get; set; } = items;

    public double Add(RawMaterial rawMaterial)
    {
        var newQuantity = SetQuantity(rawMaterial, Operation.Plus);
        Console.WriteLine($"Warehouse add {rawMaterial.GetType().Name}: {rawMaterial.Quantity}");
        return newQuantity;
    }

    public double Take(RawMaterial rawMaterial)
    {
        var newQuantity = SetQuantity(rawMaterial, Operation.Minus);
        Console.WriteLine($"Warehouse take {rawMaterial.GetType().Name}: {rawMaterial.Quantity}");
        return newQuantity;
    }

    private double CalculateQuantity(RawMaterial rawMaterial)
    {
        Thread.Sleep(RandomTimelapseGenerator.Generate());
        var item = Items[rawMaterial.GetType().Name];
        if (item == null)
            throw new InvalidOperationException("Item not found");
        return item.Quantity;
    }

    private double SetQuantity(RawMaterial rawMaterial, Operation operation)
    {
        var currentQuantity = CalculateQuantity(rawMaterial);
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
        return currentQuantity;
    }
    
    private void LogWareHouseStatus()
    {
        foreach (var item in Items)
            Console.WriteLine($"Warehouse Status {item.Key}: {item.Value.Quantity}");
    }

    private enum Operation
    {
        Plus,
        Minus
    }
}