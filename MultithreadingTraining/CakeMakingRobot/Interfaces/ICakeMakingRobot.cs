using MultithreadingTraining.Order;

namespace MultithreadingTraining.Interfaces;

public interface ICakeMakingRobot
{
    bool IsBusy { get; }
    Task<Cake.Cake> MakeCakeAsync();
    Task<IOrder> PrepareOrderAsync(IOrder order);
}