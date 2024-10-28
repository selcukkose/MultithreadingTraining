using MultithreadingTraining.Common.Helpers;
using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.Cake;

public abstract class CakeMakingPart
{
    public Dictionary<string, RawMaterial> Ingredients { get; set; }
    private List<RawMaterial> Mixture { get; } = [];
    public double Quantity => Mixture.Sum(x => x.Quantity);

    public Task<CakeMakingPart> AddAsync(RawMaterial ingredient)
    {
        Thread.Sleep(RandomTimelapseGenerator.Generate());
        Mixture.Add(ingredient);

        return  Task.FromResult(this);
    }

    public CakeMakingPart Mix()
    {
        Thread.Sleep(RandomTimelapseGenerator.Generate());
        return this;
    }
}