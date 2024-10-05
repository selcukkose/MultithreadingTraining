using MultithreadingTraining.Common.Helpers;
using MultithreadingTraining.RawMaterials;
using MultithreadingTraining.Supplier.Interfaces;
using MultithreadingTraining.WareHouse;

namespace MultithreadingTraining.Supplier;

public class IngredientSupplier : ISupplier
{
    private readonly IWareHouse _wareHouse;

    public IngredientSupplier(IWareHouse wareHouse)
    {
        _wareHouse = wareHouse;
    }

    public double Supply(RawMaterial item)
    {
        Thread.Sleep(RandomTimelapseGenerator.Generate());
        return _wareHouse.Add(item);
    }

    public double Supply(string rawMaterialName, double quantity)
    {
        Thread.Sleep(RandomTimelapseGenerator.Generate());
        return _wareHouse.Add(GenerateRawMaterial(rawMaterialName, quantity));
    }

    public void ManageWareHouseRawMaterialStatus()
    {
        while (true)
            try
            {
                const int limit = 1000;
                var itemsHasQuantityLessThanLimit = _wareHouse.Items.Where(item => item.Value.Quantity < limit);

                foreach (var item in itemsHasQuantityLessThanLimit)
                    Supply(GenerateRawMaterial(item.Key, 3000));

                Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("An error occurred while checking the warehouse status.");
                break;
            }
    }

    private static RawMaterial GenerateRawMaterial(string rawMaterialName, double quantity)

    {
        return rawMaterialName switch
        {
            nameof(Egg) => new Egg(quantity),
            nameof(Flour) => new Flour(quantity),
            nameof(Milk) => new Milk(quantity),
            nameof(Sugar) => new Sugar(quantity),
            _ => throw new InvalidOperationException("Raw material not found.")
        };
    }
}