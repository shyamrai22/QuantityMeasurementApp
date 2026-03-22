using Microsoft.Extensions.Configuration;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Repository.Database;
using QuantityMeasurementApp.Service;

namespace QuantityMeasurementApp.Controller
{
  public class ApplicationStarter : IApplicationStarter
  {
    public void StartApplication()
    {
      Console.WriteLine("Starting Quantity Measurement Application...");

      var cacheRepo = QuantityMeasurementCacheRepository.GetInstance();
      var dbRepo = CreateDatabaseRepository();

      // Optional: Sync cache → DB at startup
      SyncCacheToDatabase(cacheRepo, dbRepo);

      // Always use DB for writes, cache for reads
      var service = new QuantityMeasurementServiceImpl(
          dbRepo,
          cacheRepo
      );

      var controller = new QuantityMeasurementController(service);

      controller.Start();
    }

    private IQuantityMeasurementRepository CreateDatabaseRepository()
    {
      var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();

      string connectionString =
          configuration.GetConnectionString("DefaultConnection");

      Console.WriteLine("Connected to SQL Server Database");

      return new QuantityMeasurementDatabaseRepository(connectionString);
    }

    private void SyncCacheToDatabase(
        QuantityMeasurementCacheRepository cacheRepo,
        IQuantityMeasurementRepository dbRepo)
    {
      var cacheData = cacheRepo.GetAll();

      if (cacheData == null || cacheData.Count == 0)
      {
        Console.WriteLine("No cache data to sync.");
        return;
      }

      Console.WriteLine($"Syncing {cacheData.Count} cached records to database...");

      foreach (var record in cacheData)
      {
        dbRepo.Save(record);
      }

      cacheRepo.DeleteAll();

      Console.WriteLine("Cache synced to DB and cleared.");
    }
  }
}