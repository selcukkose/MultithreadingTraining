using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.Supplier.Interfaces;

public interface ISupplier
{
    Task<double> SupplyAsync(RawMaterial item);
    Task<double> SupplyAsync(string rawMaterialName, double quantity);
    void ManageWareHouseRawMaterialStatus();
}