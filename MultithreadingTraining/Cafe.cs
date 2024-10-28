using MultithreadingTraining.CakeMakingRobot;
using MultithreadingTraining.Order;
using MultithreadingTraining.Order.Interfaces;
using MultithreadingTraining.RawMaterials;
using MultithreadingTraining.Supplier;

namespace MultithreadingTraining;

public class Cafe
{
    private readonly Robot _cakeMakingRobot;
    private readonly IngredientSupplier _ingredientSupplier;
    private readonly IOrderManager _orderManager;
    private readonly WareHouse.WareHouse _wareHouse;

    public Cafe()
    {
        Console.WriteLine("Cafe is open.");
        _wareHouse = new WareHouse.WareHouse(new Dictionary<string, RawMaterial>
        {
            { nameof(Flour), new Flour(5000) },
            { nameof(Water), new Water(7000) },
            { nameof(Sugar), new Sugar(15000) },
            { nameof(Egg), new Egg(100) },
            { nameof(Milk), new Milk(5400) }
        });
        _cakeMakingRobot = new Robot(_wareHouse);
        _ingredientSupplier = new IngredientSupplier(_wareHouse);
        _orderManager = new OrderManager([_cakeMakingRobot], _ingredientSupplier);

        (new Thread(x => _ingredientSupplier.ManageWareHouseRawMaterialStatus())).Start();
        (new Thread(x => _orderManager.ManageOrdersAsync())).Start();
    }
}