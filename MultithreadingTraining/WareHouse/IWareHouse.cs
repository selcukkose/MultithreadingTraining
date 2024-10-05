using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.WareHouse;

public interface IWareHouse
{
    public Dictionary<string, RawMaterial> Items { get; set; }
    double Take(RawMaterial rawMaterial);
    double Add(RawMaterial rawMaterial);
}