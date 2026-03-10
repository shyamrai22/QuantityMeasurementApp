using System;
using QuantityMeasurementApp.Library.Model;

class Program
{
  static void Main()
  {
    Console.WriteLine("1. Add Lengths");
    Console.WriteLine("2. Convert Length");
    Console.WriteLine("3. Exit");

    int choice = int.Parse(Console.ReadLine());

    switch (choice)
    {
      case 1:
        var q1 = new Quantity(1.0, LengthUnit.FEET);
        var q2 = new Quantity(12.0, LengthUnit.INCH);

        var result = q1.Add(q2, LengthUnit.FEET);

        Console.WriteLine($"Result: {result.Value} {result.Unit}");
        break;

      case 2:
        double converted = Quantity.Convert(1.0, LengthUnit.FEET, LengthUnit.INCH);
        Console.WriteLine($"Converted: {converted}");
        break;
    }
  }
}