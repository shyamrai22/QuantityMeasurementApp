using QuantityMeasurementApp.Model.DTO;
using QuantityMeasurementApp.Service;

namespace QuantityMeasurementApp.Controller
{
  public class QuantityMeasurementController : IQuantityMeasurementController
  {
    private readonly IQuantityMeasurementService service;

    public QuantityMeasurementController(IQuantityMeasurementService service)
    {
      this.service = service;
    }

    public void Start()
    {
      while (true)
      {
        Console.WriteLine("\n==== Quantity Measurement Menu ====");
        Console.WriteLine("1. Compare Quantities");
        Console.WriteLine("2. Convert Quantity");
        Console.WriteLine("3. Add Quantities");
        Console.WriteLine("4. Subtract Quantities");
        Console.WriteLine("5. Divide Quantities");
        Console.WriteLine("6. Exit");

        Console.Write("Enter choice: ");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
          case 1:
            CompareMenu();
            break;

          case 2:
            ConvertMenu();
            break;

          case 3:
            AddMenu();
            break;

          case 4:
            SubtractMenu();
            break;

          case 5:
            DivideMenu();
            break;

          case 6:
            Console.WriteLine("Exiting...");
            return;

          default:
            Console.WriteLine("Invalid choice");
            break;
        }
      }
    }

    private void CompareMenu()
    {
      var q1 = ReadQuantity("first");
      var q2 = ReadQuantity("second");

      bool result = service.Compare(q1, q2);

      Console.WriteLine($"Comparison Result: {result}");
    }

    private void ConvertMenu()
    {
      var q = ReadQuantity("source");

      Console.Write("Enter target unit: ");
      string targetUnit = Console.ReadLine();

      var result = service.Convert(q, targetUnit);

      Console.WriteLine($"Converted Result: {result.Value} {result.Unit}");
    }

    private void AddMenu()
    {
      var q1 = ReadQuantity("first");
      var q2 = ReadQuantity("second");

      var result = service.Add(q1, q2);

      Console.WriteLine($"Addition Result: {result.Value} {result.Unit}");
    }

    private void SubtractMenu()
    {
      var q1 = ReadQuantity("first");
      var q2 = ReadQuantity("second");

      var result = service.Subtract(q1, q2);

      Console.WriteLine($"Subtraction Result: {result.Value} {result.Unit}");
    }

    private void DivideMenu()
    {
      var q1 = ReadQuantity("first");
      var q2 = ReadQuantity("second");

      double result = service.Divide(q1, q2);

      Console.WriteLine($"Division Result: {result}");
    }

    private QuantityDTO ReadQuantity(string label)
    {
      Console.Write($"Enter {label} value: ");
      double value = double.Parse(Console.ReadLine());

      Console.Write($"Enter {label} unit: ");
      string unit = Console.ReadLine();

      Console.Write($"Enter measurement type (Length/Weight/Volume/Temperature): ");
      string type = Console.ReadLine();

      return new QuantityDTO(value, unit, type);
    }
  }
}
