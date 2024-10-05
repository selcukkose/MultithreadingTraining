using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.Cake;

public class Cream : CakeMakingPart
{
    public Cream()
    {
        Ingredients = new Dictionary<string, RawMaterial>
        {
            { nameof(Flour), new Flour(200) },
            { nameof(Milk), new Milk(450) },
            { nameof(Sugar), new Sugar(550) },
            { nameof(Egg), new Egg(1) }
        };
    }
}