using MultithreadingTraining.RawMaterials;

namespace MultithreadingTraining.Supplier.Interfaces;

public interface ISupplier
{
    double Supply(RawMaterial item);
    double Supply(string rawMaterialName, double quantity);
    void ManageWareHouseRawMaterialStatus();
}