using QuantityMeasurementApp.Model.Entity;

namespace QuantityMeasurementApp.Repository
{
  public interface IQuantityMeasurementRepository
  {
    void Save(QuantityMeasurementEntity entity);
    void DeleteAll();
    List<QuantityMeasurementEntity> GetAll();
  }
}