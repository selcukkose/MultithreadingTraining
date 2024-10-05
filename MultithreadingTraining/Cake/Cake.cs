using MultithreadingTraining.Common.Helpers;

namespace MultithreadingTraining.Cake;

public class Cake
{
    public List<CakeMakingPart> Parts { get; } = [];

    public Cake Add(CakeMakingPart ingredient)
    {
        Thread.Sleep(RandomTimelapseGenerator.Generate());
        Parts.Add(ingredient);

        return this;
    }
}