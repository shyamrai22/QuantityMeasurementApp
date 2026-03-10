using System;
using QuantityMeasurementApp.Library.Model;

class Program
{
  static void Main()
  {
    Console.WriteLine("1. Add Lengths");
    Console.WriteLine("2. Convert Length");
    Console.WriteLine("3. Exit");

    int choice = int.Parse(Console.ReadLine() ?? "0");

    switch (choice)
    {
      case 1:
        var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCH);

        var result = q1.Add(q2);

        Console.WriteLine($"Result: {result.Value} {result.Unit}");
        break;

      case 2:
        var length = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);

        var converted = length.ConvertTo(LengthUnit.INCH);

        Console.WriteLine($"Converted: {converted.Value} {converted.Unit}");
        break;

      case 3:
        return;
    }
  }
}