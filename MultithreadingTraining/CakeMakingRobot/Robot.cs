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

    public Cake.Cake MakeCake()
    {
        Console.WriteLine("Making cake...");
        IsBusy = true;
        var cake = new Cake.Cake();

        try
        {
            var pastry = _pastryPreparer.Prepare();
            cake.Add(pastry);
            var cream = _creamPreparer.Prepare();
            cake.Add(cream);
            var chocolate = _chocolatePreparer.Prepare();
            cake.Add(chocolate);
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

    public IOrder PrepareOrder(IOrder order)
    {
        var products = order.OrderLines.Select(orderLine => MakeCake()).ToList();

        order.Products = products;
        return order;
    }
}