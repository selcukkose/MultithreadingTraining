namespace MultithreadingTraining.Order.Interfaces;

public interface IOrderManager
{
    Queue<Order> OrderQueue { get; }
    Task ManageOrdersAsync();
}