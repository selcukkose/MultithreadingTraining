using MultithreadingTraining.Order;

namespace MultithreadingTraining.Interfaces;

public interface ICakeMakingRobot
{
    bool IsBusy { get; }
    Cake.Cake MakeCake();
    IOrder PrepareOrder(IOrder order);
}