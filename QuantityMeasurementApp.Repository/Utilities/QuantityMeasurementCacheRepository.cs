using QuantityMeasurementApp.Model.Entity;

namespace QuantityMeasurementApp.Repository
{
  public class QuantityMeasurementCacheRepository
      : IQuantityMeasurementRepository
  {
    private static QuantityMeasurementCacheRepository instance;

    private List<QuantityMeasurementEntity> storage = new();

    private QuantityMeasurementCacheRepository() { }

    public static QuantityMeasurementCacheRepository GetInstance()
    {
      if (instance == null)
        instance = new QuantityMeasurementCacheRepository();

      return instance;
    }

    public void Save(QuantityMeasurementEntity entity)
    {
      storage.Add(entity);
    }

    public List<QuantityMeasurementEntity> GetAll()
    {
      return storage;
    }
  }
}