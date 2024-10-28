using MultithreadingTraining.CakeMakingRobot;
using MultithreadingTraining.Order;
using MultithreadingTraining.Order.Interfaces;
using MultithreadingTraining.RawMaterials;
using MultithreadingTraining.Supplier;

namespace MultithreadingTraining;

public class Cafe
{
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
        _ingredientSupplier = new IngredientSupplier(_wareHouse);
        _orderManager = new OrderManager([new Robot(_wareHouse), new Robot(_wareHouse)], _ingredientSupplier);

        /*
         * For the following long running operations Task.Factory.StartNew or Thread.QueueUserWorkItem can not be used.
         * Because these methods use ThreadPool and ThreadPool is not suitable for long running operations.
         * The operations running by the ThreadPool mey be terminated by the CLR unexpectedly.
         * For more information the following link can be visited: https://sergeyteplyakov.github.io/Blog/async/2019/05/21/The-Dangers-of-Task.Factory.StartNew.html
         */
        (new Thread(x => _ingredientSupplier.ManageWareHouseRawMaterialStatus())).Start();
        (new Thread(x => _orderManager.ManageOrdersAsync())).Start();
    }
}