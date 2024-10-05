namespace MultithreadingTraining.Order;

public interface IOrder
{
    List<Cake.Cake> OrderLines { get; set; }
    List<Cake.Cake> Products { get; set; }
}