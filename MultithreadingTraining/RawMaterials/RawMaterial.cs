namespace MultithreadingTraining.RawMaterials;

public abstract class RawMaterial(double _quantity)
{
    public double Quantity { get; set; } = _quantity;
}