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
        Console.WriteLine("6. View All Records"); // NEW
        Console.WriteLine("7. Exit");

        Console.Write("Enter choice: ");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
          Console.WriteLine("Invalid choice");
          continue;
        }

        try // GLOBAL TRY-CATCH
        {
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
              ViewAllMenu(); // NEW
              break;

            case 7:
              return;

            default:
              Console.WriteLine("Invalid choice");
              break;
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error: {ex.Message}");
        }
      }
    }

    private void CompareMenu()
    {
      var first = ReadQuantity("first");
      var second = ReadQuantity("second");

      bool result = service.Compare(first, second);

      Console.WriteLine($"Result: {result}");
    }

    private void ConvertMenu()
    {
      var quantity = ReadQuantity("source");

      Console.Write("Enter target unit: ");
      string targetUnit = Console.ReadLine();

      var result = service.Convert(quantity, targetUnit);

      Console.WriteLine($"Converted Result: {result.Value} {result.Unit}");
    }

    private void AddMenu()
    {
      var first = ReadQuantity("first");
      var second = ReadQuantity("second");

      var result = service.Add(first, second);

      Console.WriteLine($"Addition Result: {result.Value} {result.Unit}");
    }

    private void SubtractMenu()
    {
      var first = ReadQuantity("first");
      var second = ReadQuantity("second");

      var result = service.Subtract(first, second);

      Console.WriteLine($"Subtraction Result: {result.Value} {result.Unit}");
    }

    private void DivideMenu()
    {
      var first = ReadQuantity("first");
      var second = ReadQuantity("second");

      double result = service.Divide(first, second);

      Console.WriteLine($"Division Result: {result}");
    }

    // NEW FEATURE (IMPORTANT)
    private void ViewAllMenu()
    {
      var data = service.GetAllMeasurements();

      Console.WriteLine("\n==== Stored Records ====");

      foreach (var item in data)
      {
        Console.WriteLine(
          $"{item.Operation} | {item.Operand1} | {item.Operand2} | {item.Result} | {item.MeasurementType} | Error: {item.HasError} | {item.ErrorMessage}"
        );
      }
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