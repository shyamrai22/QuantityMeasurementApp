using System.Text.Json;
using QuantityMeasurementApp.Model.Entity;

namespace QuantityMeasurementApp.Repository
{
  public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
  {
    private static QuantityMeasurementCacheRepository instance;

    private List<QuantityMeasurementEntity> storage = new();

    private readonly string filePath = "measurements.json";

    private QuantityMeasurementCacheRepository()
    {
      LoadFromFile();
    }

    public static QuantityMeasurementCacheRepository GetInstance()
    {
      if (instance == null)
        instance = new QuantityMeasurementCacheRepository();

      return instance;
    }

    public List<QuantityMeasurementEntity> GetAll()
    {
      return storage;
    }

    public void Save(QuantityMeasurementEntity entity)
    {
      storage.Add(entity);
      SaveToFile();
    }

    public void DeleteAll()
    {
      storage.Clear();
      File.WriteAllText(filePath, "[]");
    }

    private void SaveToFile()
    {
      var json = JsonSerializer.Serialize(storage, new JsonSerializerOptions
      {
        WriteIndented = true
      });

      File.WriteAllText(filePath, json);
    }

    private void LoadFromFile()
    {
      if (!File.Exists(filePath))
        return;

      var json = File.ReadAllText(filePath);

      var data = JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(json);

      if (data != null)
        storage = data;
    }

    public void ClearCache()
    {
      storage.Clear();
    }

  }
}