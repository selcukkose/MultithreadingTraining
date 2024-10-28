using MultithreadingTraining.CakeMakingRobot;
using MultithreadingTraining.Common.Exceptions;
using MultithreadingTraining.Interfaces;
using MultithreadingTraining.Order.Interfaces;
using MultithreadingTraining.Supplier.Interfaces;

namespace MultithreadingTraining.Order;

public class OrderManager(List<ICakeMakingRobot> robots, ISupplier supplier) : IOrderManager
{
    public Queue<Order> OrderQueue { get; } = new();

    public async Task ManageOrdersAsync()
    {
        (new Thread(ListenForOrders)).Start();
        while (true)
            try
            {
                if (OrderQueue.Count == 0)
                {
                    Thread.Sleep(100);
                    continue;
                }

                Parallel.ForEach(robots, async robot => await ManageRobot(robot));
            }
            catch
            {
                break;
            }
    }

    private async Task ManageRobot(ICakeMakingRobot robot)
    {
        if (robot.IsBusy || OrderQueue.Count == 0)
            return;
        
        var order = OrderQueue.Dequeue();
        try
        {
            await robot?.PrepareOrderAsync(order);
        }
        catch (NotEnoughRawMaterialException e)
        {
            Console.WriteLine(e.Message);
            OrderQueue.Enqueue(order);
            await supplier.SupplyAsync(e.RawMaterialName, e.RequiredQuantity);
        }
    }

    private void ListenForOrders()
    {
        while (true)
        {
            try
            {
                OrderQueue.Enqueue(new Order()
                {
                    OrderLines = [new Cake.Cake()]
                });
                Console.WriteLine("Order received.");
                Thread.Sleep(5000);
            }
            catch
            {
                break;
            }
        }
    }

    private List<ICakeMakingRobot> GetAvailableRobots()
    {
        return robots.Where(r => !r.IsBusy).ToList();
    }
}