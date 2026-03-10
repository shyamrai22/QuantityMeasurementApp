using System;
using QuantityMeasurementApp.Library.Model;

class Program
{
  static void Main()
  {
    var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
    var v2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);

    Console.WriteLine($"Equality: {v1.Equals(v2)}");

    var converted = v1.ConvertTo(VolumeUnit.MILLILITRE);
    Console.WriteLine($"Conversion: {converted.Value} {converted.Unit}");

    var added = v1.Add(v2);
    Console.WriteLine($"Addition: {added.Value} {added.Unit}");
  }
}
