using MultithreadingTraining.Common.Helpers;
using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.Cake;

public abstract class CakeMakingPart
{
    public Dictionary<string, RawMaterial> Ingredients { get; set; }
    private List<RawMaterial> Mixture { get; } = [];
    public double Quantity => Mixture.Sum(x => x.Quantity);

    public CakeMakingPart Add(RawMaterial ingredient)
    {
        Thread.Sleep(RandomTimelapseGenerator.Generate());
        Mixture.Add(ingredient);

        return this;
    }

    public CakeMakingPart Mix()
    {
        Thread.Sleep(RandomTimelapseGenerator.Generate());
        return this;
    }
}