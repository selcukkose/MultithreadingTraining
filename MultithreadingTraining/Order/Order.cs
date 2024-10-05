namespace MultithreadingTraining.Order;

public class Order : IOrder
{
    public List<Cake.Cake> OrderLines { get; set; }
    public List<Cake.Cake> Products { get; set; }
}