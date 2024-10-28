using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.WareHouse;

public interface IWareHouse
{
    public Dictionary<string, RawMaterial> Items { get; set; }
    Task<double> TakeAsync(RawMaterial rawMaterial);
    Task<double> AddAsync(RawMaterial rawMaterial);
}