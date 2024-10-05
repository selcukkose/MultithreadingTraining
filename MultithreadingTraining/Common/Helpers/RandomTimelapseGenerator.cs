namespace MultithreadingTraining.Common.Helpers;

public static class RandomTimelapseGenerator
{
    public static int Generate()
    {
        return new Random().Next(100, 300);
    }
}