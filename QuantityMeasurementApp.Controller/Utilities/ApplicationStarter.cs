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
      Console.WriteLine("Select Storage Option:");
      Console.WriteLine("1. Cache (JSON)");
      Console.WriteLine("2. Database (SQL Server)");
      Console.Write("Enter choice: ");

      if (!int.TryParse(Console.ReadLine(), out int choice))
      {
        Console.WriteLine("Invalid input");
        return;
      }

      IQuantityMeasurementRepository repository =
          CreateRepository(choice);

      var service = new QuantityMeasurementServiceImpl(repository);

      var controller = new QuantityMeasurementController(service);

      controller.Start();
    }

    private IQuantityMeasurementRepository CreateRepository(int choice)
    {
      var cacheRepo = QuantityMeasurementCacheRepository.GetInstance();

      switch (choice)
      {
        case 1:
          Console.WriteLine("Using Cache Repository (JSON)");
          return cacheRepo;

        case 2:
          Console.WriteLine("Using Database Repository (SQL Server)");

          var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();

          string connectionString =
              configuration.GetConnectionString("DefaultConnection");

          var dbRepo = new QuantityMeasurementDatabaseRepository(connectionString);

          //  Sync cache → DB before using DB
          SyncCacheToDatabase(cacheRepo, dbRepo);

          return dbRepo;

        default:
          throw new ArgumentException("Invalid choice");
      }
    }

    private void SyncCacheToDatabase(
        IQuantityMeasurementRepository cacheRepo,
        IQuantityMeasurementRepository dbRepo)
    {
      var cacheData = cacheRepo.GetAll();

      if (cacheData == null || cacheData.Count == 0)
      {
        Console.WriteLine("No cache data to sync.");
        return;
      }

      Console.WriteLine($"Syncing {cacheData.Count} records to database...");

      foreach (var record in cacheData)
      {
        dbRepo.Save(record);
      }

      cacheRepo.DeleteAll();

      Console.WriteLine("Sync completed. Cache cleared.");
    }
  }
}