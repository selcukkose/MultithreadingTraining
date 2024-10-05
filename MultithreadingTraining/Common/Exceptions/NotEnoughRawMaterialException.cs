namespace MultithreadingTraining.Common.Exceptions;

public class NotEnoughRawMaterialException(string rawMaterialName, double requiredQuantity)
    : Exception($"Not enough RawMaterial, {rawMaterialName}")
{
    public readonly string RawMaterialName = rawMaterialName;
    public readonly double RequiredQuantity = requiredQuantity;
}