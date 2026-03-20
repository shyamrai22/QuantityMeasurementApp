namespace QuantityMeasurementApp.Controller;

class Program
{
  static void Main()
  {
    IApplicationStarter app = new ApplicationStarter();
    app.StartApplication();
  }
}