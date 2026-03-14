using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Service;

namespace QuantityMeasurementApp.Controller
{
  public class ApplicationStarter : IApplicationStarter
  {
    public void StartApplication()
    {
      var repository =
          QuantityMeasurementCacheRepository.GetInstance();

      var service =
          new QuantityMeasurementServiceImpl(repository);

      var controller =
          new QuantityMeasurementController(service);

      controller.Start();
    }
  }
}