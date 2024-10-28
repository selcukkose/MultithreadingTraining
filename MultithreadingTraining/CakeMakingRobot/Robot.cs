using MultithreadingTraining.Cake;
using MultithreadingTraining.CakeMakingRobot.CakePartMaking;
using MultithreadingTraining.Interfaces;
using MultithreadingTraining.Order;

namespace MultithreadingTraining.CakeMakingRobot;

public class Robot : ICakeMakingRobot
{
    private readonly CakePartPreparer<Chocolate> _chocolatePreparer;
    private readonly CakePartPreparer<Cream> _creamPreparer;
    private readonly CakePartPreparer<Pastry> _pastryPreparer;

    public Robot(WareHouse.WareHouse wareHouse)
    {
        _pastryPreparer = new CakePartPreparer<Pastry>(wareHouse);
        _chocolatePreparer = new CakePartPreparer<Chocolate>(wareHouse);
        _creamPreparer = new CakePartPreparer<Cream>(wareHouse);
    }

    public bool IsBusy { get; private set; }

    public async Task<Cake.Cake> MakeCakeAsync()
    {
        Console.WriteLine("Making cake...");
        IsBusy = true;
        var cake = new Cake.Cake();

        try
        {
            /***
             * For the following operation Parallel.Invoke method could be used but it is not suitable for async operations.
             */
            var pastry = _pastryPreparer.PrepareAsync();
            var cream = _creamPreparer.PrepareAsync();
            var chocolate = _chocolatePreparer.PrepareAsync();
            cake.Add(await pastry);
            cake.Add(await cream);
            cake.Add(await chocolate);
        }
        catch (Exception e)
        {
            IsBusy = false;
            throw;
        }

        IsBusy = false;
        Console.WriteLine("Cake is ready.");
        return cake;
    }

    public async Task<IOrder> PrepareOrderAsync(IOrder order)
    {
        var products = order.OrderLines.Select(orderLine => MakeCakeAsync()).ToList();

        order.Products = (await Task.WhenAll(products)).ToList();
        return order;
    }
}