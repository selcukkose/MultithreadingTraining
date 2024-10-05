using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.Cake;

public class Pastry : CakeMakingPart
{
    public Pastry()
    {
        Ingredients = new Dictionary<string, RawMaterial>
        {
            { nameof(Flour), new Flour(500) },
            { nameof(Milk), new Milk(300) },
            { nameof(Sugar), new Sugar(250) },
            { nameof(Egg), new Egg(2) },
            { nameof(Water), new Water(100) }
        };
    }
}