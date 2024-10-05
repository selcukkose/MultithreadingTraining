using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.Cake;

public class Chocolate : CakeMakingPart
{
    public Chocolate()
    {
        Ingredients = new Dictionary<string, RawMaterial>
        {
            { nameof(Flour), new Flour(100) },
            { nameof(Milk), new Milk(500) },
            { nameof(Sugar), new Sugar(350) }
        };
    }
}