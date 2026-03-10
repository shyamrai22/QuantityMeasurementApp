using System;
using QuantityMeasurementApp.Library.Model;

class Program
{
  static void Main()
  {
    var length1 = new Quantity<LengthUnit>(10, LengthUnit.FEET);
    var length2 = new Quantity<LengthUnit>(6, LengthUnit.INCH);

    var subtraction = length1.Subtract(length2);
    Console.WriteLine($"Subtraction: {subtraction.Value} {subtraction.Unit}");

    var division = length1.Divide(new Quantity<LengthUnit>(2, LengthUnit.FEET));
    Console.WriteLine($"Division: {division}");
  }
}
